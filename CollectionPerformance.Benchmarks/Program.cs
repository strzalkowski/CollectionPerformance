using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace CollectionPerformance.Benchmarks;

public class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<PermissionBenchmark>();
    }
}

public enum Permission
{
    ViewDashboard,
    EditProfile,
    DeleteUser,
    ManageRoles,
    ViewReports,
    ExportData,
    ImportData,
    ManageSettings,
    ViewLogs,
    ClearCache,
    ApproveRequests,
    RejectRequests,
    CreateContent,
    UpdateContent,
    DeleteContent,
    PublishContent,
    ArchiveContent,
    ViewAnalytics,
    ManageSubscriptions,
    BillingAccess,
    SupportChat,
    ManageFaq,
    UploadFiles,
    DeleteFiles,
    DownloadInvoices,
    ManageApiKeys,
    ViewAuditLog,
    SystemRestart,
    DatabaseBackup,
    DatabaseRestore,
    ManagePlugins,
    ViewMetrics,
    EditTemplates,
    SendNotifications,
    ReceiveAlerts,
    InviteUsers,
    RemoveUsers,
    ResetPasswords,
    UpdateSecurityPolicy,
    ViewServerStatus,
    AccessBetaFeatures,
    ManageWorkflows,
    ReviewCode,
    DeployApplication,
    RollbackDeployment,
    ViewHardwareStats,
    ManageIpWhitelist,
    ConfigureSso,
    ViewUsageQuotas,
    RequestCredit,
    TransferOwnership,
    CloseAccount
}

[MemoryDiagnoser]
public class PermissionBenchmark
{
    [Params("Small", "Medium", "Large")]
    public string SetSize { get; set; } = "Small";

    [Params("Start", "End", "None")]
    public string MatchCase { get; set; } = "None";

    private List<Permission> _userPermissionsList = null!;
    private HashSet<Permission> _userPermissionsSet = null!;
    private List<Permission> _requiredPermissions = null!;

    [GlobalSetup]
    public void Setup()
    {
        int userCount = SetSize switch
        {
            "Small" => 5,
            "Medium" => 100,
            "Large" => 1000,
            _ => 5
        };

        int requiredCount = SetSize switch
        {
            "Small" => 3,
            "Medium" => 20,
            "Large" => 100,
            _ => 3
        };

        // Generujemy unikalne uprawnienia dla użytkownika
        // Używamy rzutowania int na Permission, aby wygenerować dużą liczbę unikalnych wartości,
        // nawet jeśli enum ma ich tylko 52. To symuluje duże kolekcje unikalnych identyfikatorów.
        _userPermissionsList = Enumerable.Range(1, userCount)
            .Select(i => (Permission)i)
            .ToList();
        
        _userPermissionsSet = new HashSet<Permission>(_userPermissionsList);

        // Przygotowujemy wymagane uprawnienia w zależności od przypadku
        _requiredPermissions = new List<Permission>();
        
        if (MatchCase == "Start")
        {
            // Dopasowanie na początku - pierwsze wymagane jest pierwszym użytkownika
            _requiredPermissions.Add(_userPermissionsList[0]);
            for (int i = 1; i < requiredCount; i++)
            {
                // Reszta to wartości poza zakresem użytkownika
                _requiredPermissions.Add((Permission)(userCount + i + 1000));
            }
        }
        else if (MatchCase == "End")
        {
            // Dopasowanie na końcu - ostatnie wymagane jest ostatnim użytkownika
            for (int i = 0; i < requiredCount - 1; i++)
            {
                _requiredPermissions.Add((Permission)(userCount + i + 1000));
            }
            _requiredPermissions.Add(_userPermissionsList[^1]);
        }
        else // None
        {
            // Brak dopasowania
            for (int i = 0; i < requiredCount; i++)
            {
                _requiredPermissions.Add((Permission)(userCount + i + 1000));
            }
        }
    }

    // 1. UserPermissions.Intersect(RequiredPermissions).Any()
    [Benchmark]
    public bool IntersectAny()
    {
        return _userPermissionsList.Intersect(_requiredPermissions).Any();
    }

    // 2. RequiredPermissions.Any(p => UserPermissions.Contains(p)) gdzie UserPermissions to List<PermissionType>.
    [Benchmark]
    public bool ListAnyContains()
    {
        return _requiredPermissions.Any(p => _userPermissionsList.Contains(p));
    }

    // 3. RequiredPermissions.Any(p => UserPermissionsSet.Contains(p)) gdzie UserPermissionsSet to HashSet<PermissionType>.
    [Benchmark]
    public bool HashSetAnyContains()
    {
        /* 
           Dlaczego HashSet jest zazwyczaj szybszy przy dużych kolekcjach?
           HashSet<T> wykorzystuje tablicę mieszającą (hash table), co pozwala na sprawdzenie obecności elementu (Contains) 
           w czasie zbliżonym do O(1), niezależnie od liczby elementów w zbiorze. 
           W przypadku List<T>, metoda Contains musi przeszukać listę sekwencyjnie, co zajmuje czas O(n).
           Przy dużych zestawach danych (np. 1000 uprawnień), różnica między dostępem stałym a liniowym staje się znacząca.
        */
        return _requiredPermissions.Any(p => _userPermissionsSet.Contains(p));
    }

    // 4. Klasyczna pętla foreach z wczesnym wyjściem (return true).
    [Benchmark]
    public bool ForeachEarlyExit()
    {
        foreach (var required in _requiredPermissions)
        {
            foreach (var user in _userPermissionsList)
            {
                if (user == required)
                {
                    return true;
                }
            }
        }
        return false;
    }
}

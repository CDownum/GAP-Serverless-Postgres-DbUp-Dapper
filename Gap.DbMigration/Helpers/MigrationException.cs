using DbUp.Engine;

namespace GAP.DbMigration.Helpers
{
    public class MigrationException : Exception
    {
        public IEnumerable<string> Scripts { get; }

        public MigrationException(DatabaseUpgradeResult dbUpgradeResult)
            : base(dbUpgradeResult.Error.Message, dbUpgradeResult.Error)
        {
            Scripts = dbUpgradeResult.Scripts.Select(s => s.Name).ToList();
        }
    }
}
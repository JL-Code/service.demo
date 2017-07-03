using System;

namespace mecode.toolkit
{
    public class UpgradeService : IDisposable
    {
        private EFDbContext _dbContext;
        public UpgradeService(string connstr)
        {
            _dbContext = new EFDbContext(connstr);
        }

        public void Insert()
        {
            throw new NotImplementedException();
        }


        public void UpdateUpgradeStatus(UpgradeLog entity)
        {
            var old = GetUpgradeLog(entity.UpgradeLogGUID);
            if (old != null)
            {
                old.IsUpgraded = entity.IsUpgraded;
                old.UpgradeDate = entity.UpgradeDate;
                _dbContext.SaveChanges();
            }
        }

        public UpgradeLog GetUpgradeLog(Guid guid)
        {
            var entity = _dbContext.UpgradeLogs.Find(guid);
            return entity;
        }

        public bool DeduceIsUpgraded(Guid guid)
        {
            var entity = _dbContext.UpgradeLogs.Find(guid);
            return entity == null ? true : entity.IsUpgraded;
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}

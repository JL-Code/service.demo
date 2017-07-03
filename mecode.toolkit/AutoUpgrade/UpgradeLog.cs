using System;

namespace mecode.toolkit
{
    public class UpgradeLog
    {
        public Guid UpgradeLogGUID { get; set; }

        public string VersionName { get; set; }

        public string Version { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public DateTime? UpgradeDate { get; set; }

        public bool IsDownloaded { get; set; }

        public bool IsUpgraded { get; set; }

        public string Remark { get; set; }
    }
}

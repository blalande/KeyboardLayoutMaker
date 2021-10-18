using System.Collections.Generic;

namespace textAnalyzer {
    public class StatsPresenter {
        public IEnumerable<Stats> chars { get; set; }
        public IEnumerable<Stats> bigrams { get; set; }
        public IEnumerable<Stats> trigrams { get; set; }
                
    }
}
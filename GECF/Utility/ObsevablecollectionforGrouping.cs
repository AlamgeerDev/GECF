using System;
using System.Collections.ObjectModel;

namespace GECF.Utility
{
	public class ObsevablecollectionforGrouping<K, T>: ObservableCollection<T>
    {
        private readonly K _key;

        public ObsevablecollectionforGrouping()
        {
        }

        public ObsevablecollectionforGrouping(IGrouping<K, T> group)
            : base(group)
        {
            _key = group.Key;
        }

        public K Key
        {
            get { return _key; }
        }
    }
}


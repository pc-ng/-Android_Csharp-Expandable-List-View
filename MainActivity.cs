using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;

namespace Expandable_List_View
{
    [Activity(Label = "Expandable_List_View", MainLauncher = true)]
    public class MainActivity : Activity
    {
        ExpandableListView expandableList;
        Dictionary<string, List<string>> groupItems = new Dictionary<string, List<string>>();
        List<string> lstKeys = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            expandableList = FindViewById<ExpandableListView> (Resource.Id.expandableListView);

            CreateExpendableListData();
            expandableList.SetAdapter(new ExpandableListAdapter(this, groupItems));
            expandableList.ChildClick += ExpandableList_ChildClick;
        }

        private void ExpandableList_ChildClick(object sender, ExpandableListView.ChildClickEventArgs e)
        {
            var itmGroup = lstKeys[e.GroupPosition];
            var itmChild = groupItems[itmGroup][e.ChildPosition];

            Toast.MakeText(this, string.Format("Group {0}: child {1}", itmGroup, itmChild), ToastLength.Long).Show();
        }

        void CreateExpendableListData()
        {
            for (int iGroup = 1; iGroup <= 3; iGroup++)
            {
                var lstChild = new List<string>();
                for (int iChild = 1; iChild <= 3; iChild++)
                {
                    lstChild.Add(string.Format("Group {0} Child {1}", iGroup, iChild));
                }
                groupItems.Add(string.Format("Group {0}", iGroup), lstChild);
            }
            lstKeys = new List<string>(groupItems.Keys);
        }
    }
}


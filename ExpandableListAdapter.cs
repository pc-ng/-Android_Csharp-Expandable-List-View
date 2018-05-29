using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Expandable_List_View
{
    class ExpandableListAdapter : BaseExpandableListAdapter
    {
        Dictionary<string, List<string>> groupItems;
        List<string> groupKeys;
        Activity activity;

        public ExpandableListAdapter(Activity activity, Dictionary<string, List<string>> groupItems)
        {
            this.groupItems = groupItems;
            this.activity = activity;
            this.groupKeys = groupItems.Keys.ToList();
        }

        public override int GroupCount
        {
            get{ return groupItems.Count; }
        }

        public override bool HasStableIds
        {
            get { return true; }
        }

        /// <summary>
        /// Handle the child items
        /// </summary>
        /// <param name="groupPosition"></param>
        /// <param name="childPosition"></param>
        /// <returns></returns>
        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return groupItems[groupKeys[groupPosition]][childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return groupItems[groupKeys[groupPosition]].Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            var item = groupItems[groupKeys[groupPosition]][childPosition];

            if (convertView == null)
                convertView = activity.LayoutInflater.Inflate(Resource.Layout.ChildList, null);

            var textBox = convertView.FindViewById<TextView>(Resource.Id.txtSmall);
            textBox.SetText(item, TextView.BufferType.Normal);

            return convertView;
        }

        /// <summary>
        /// Handle the group items
        /// </summary>
        /// <param name="groupPosition"></param>
        /// <returns></returns>
        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return groupKeys[groupPosition];
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            var item = groupKeys[groupPosition];

            if (convertView == null)
                convertView = activity.LayoutInflater.Inflate(Resource.Layout.GroupList, null);

            var textBox = convertView.FindViewById<TextView>(Resource.Id.txtLarge);
            textBox.SetText(item, TextView.BufferType.Normal);

            return convertView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}
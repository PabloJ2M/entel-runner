using System;

namespace Unity.Customization
{
    using Services.CloudSave;

    [Serializable]
    public class ItemsRemote
    {
        public string date;
        public SerializedNestedHashSet discounts;
    }

    public struct ItemsCloud : IJsonData
    {
        public NestedHashSet discounts_list;

        public ItemsCloud(ItemDictionary dictionary)
        {
            discounts_list = new();

            foreach (var field in dictionary)
            {
                string fieldKey = field.Key.ID;
                if (!discounts_list.ContainsKey(fieldKey))
                    discounts_list[fieldKey] = new();

                foreach (var item in field.Value.items)
                {
                    if (item.Cost == 0) continue;

                    if (!discounts_list[fieldKey].ContainsKey(item.Group))
                        discounts_list[fieldKey][item.Group] = new();

                    discounts_list[fieldKey][item.Group].Add(item.ID);
                }
            }
        }
    }
}
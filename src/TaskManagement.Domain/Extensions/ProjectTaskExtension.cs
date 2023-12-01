using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Extensions
{
    public static class ProjectTaskExtension
    {
        public static IEnumerable<Historic> BuildHistoric(this ProjectTask newData,
                                                                                                int lastUpdateUser, 
                                                                                                ProjectTask oldData = null)
        {
            var historics = new List<Historic>();
            var properties = newData.GetType().GetProperties().Where(x => x.Name != "Id" && x.Name != "Project");
            foreach (var prop in properties)
            {
                var oldValue = (oldData != null) ? oldData?.GetType()?.GetProperty(prop.Name)?.GetValue(oldData, null)?.ToString() ?? "" : "";
                var newValue = prop?.GetValue(newData, null)?.ToString();

                if (oldValue != newValue)
                {
                    historics.Add(new Historic
                    {
                        UserId = lastUpdateUser,
                        ProjectTaskId = newData.Id,
                        UpdateDate = DateTime.UtcNow,
                        PropertyName = prop?.Name,
                        OldValue = oldValue,
                        NewValue = newValue
                    });
                }
            }
            return historics;
        }
    }
}

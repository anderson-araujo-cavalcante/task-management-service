using System.Reflection;
using System.Xml.Linq;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Domain.Extensions
{
    public static class Historic2<TEntity> where TEntity : class
    {
        public static IEnumerable<Historic> Build<TEntity>(TEntity newData,
                                                                                            TEntity oldData,
                                                                                             int lastUpdateUser,
                                                                                             int projectTaskId,
                                                                                             Func<PropertyInfo, bool> predicate = null)
        {
            var historics = new List<Historic>();
            var properties = predicate != null ? newData.GetType().GetProperties().Where(predicate) : newData.GetType().GetProperties().Where(x => x.Name != "Id");
            foreach (var prop in properties)
            {
                var oldValue = (oldData != null) ? oldData?.GetType()?.GetProperty(prop.Name)?.GetValue(oldData, null)?.ToString() ?? "" : "";
                var newValue = prop?.GetValue(newData, null)?.ToString();

                if (oldValue != newValue)
                {
                    historics.Add(new Historic
                    {
                        UserId = lastUpdateUser,
                        ProjectTaskId = projectTaskId,
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
    //public static class ProjectTaskExtension
    //{
    //    public static IEnumerable<Historic> BuildHistoric(this ProjectTask newData,
    //                                                                                            int lastUpdateUser, 
    //                                                                                            ProjectTask oldData = null)
    //    {
    //        var historics = new List<Historic>();
    //        var properties = newData.GetType().GetProperties().Where(x => x.Name != "Id" && x.Name != "Project");
    //        //var properties = newData.GetType().GetProperties().Where(x => x.Name != "Id" && x.Name != "Project");
    //        foreach (var prop in properties)
    //        {
    //            var oldValue = (oldData != null) ? oldData?.GetType()?.GetProperty(prop.Name)?.GetValue(oldData, null)?.ToString() ?? "" : "";
    //            var newValue = prop?.GetValue(newData, null)?.ToString();

    //            if (oldValue != newValue)
    //            {
    //                historics.Add(new Historic
    //                {
    //                    UserId = lastUpdateUser,
    //                    ProjectTaskId = newData.Id,
    //                    UpdateDate = DateTime.UtcNow,
    //                    PropertyName = prop?.Name,
    //                    OldValue = oldValue,
    //                    NewValue = newValue
    //                });
    //            }
    //        }
    //        return historics;
    //    }

    //    public static IEnumerable<Historic> BuildHistoric2<TEntity>(<TEntity> newData,
    //                                                                                            int lastUpdateUser,
    //                                                                                            Func<TEntity, bool> predicate,
    //                                                                                            ProjectTask oldData = null)
    //    {
    //        var historics = new List<Historic>();
    //        var properties = newData.GetType().GetProperties().Where(x => x.Name != "Id" && x.Name != "Project");
    //        //var properties = newData.GetType().GetProperties().Where(x => x.Name != "Id" && x.Name != "Project");
    //        foreach (var prop in properties)
    //        {
    //            var oldValue = (oldData != null) ? oldData?.GetType()?.GetProperty(prop.Name)?.GetValue(oldData, null)?.ToString() ?? "" : "";
    //            var newValue = prop?.GetValue(newData, null)?.ToString();

    //            if (oldValue != newValue)
    //            {
    //                historics.Add(new Historic
    //                {
    //                    UserId = lastUpdateUser,
    //                    ProjectTaskId = newData.Id,
    //                    UpdateDate = DateTime.UtcNow,
    //                    PropertyName = prop?.Name,
    //                    OldValue = oldValue,
    //                    NewValue = newValue
    //                });
    //            }
    //        }
    //        return historics;
    //    }
    //}
}

﻿
using System;
using MongoDB.Bson;
using MongoDB.Driver.Extensions.EventListeners.Tests.Entities;

namespace MongoDB.Driver.Extensions.EventListeners.Tests
{
    public sealed class DefaultSaveEventListener : ISaveEventListener
    {
        public void OnSave(ISaveEventArgs @event)
        {
            if(@event.Entity != null && @event.ContextData != null)
            {
                var entity = @event.Entity as IEntity;
                if(entity != null)
                {
                    bool isTransient = false;
                    if (entity.Id == ObjectId.Empty)
                    {
                        entity.Id = ObjectId.GenerateNewId();
                        isTransient = true;
                    }

                    var now = DateTime.UtcNow;

                    var contextData = @event.ContextData as CurrentContextData;
                    if(contextData != null)
                    {
                        if (isTransient)
                        {
                            var creatable = entity as IInsertable;
                            if (creatable != null)
                            {
                                creatable.CreatedOn = now;
                                creatable.CreatedBy = contextData.CurrentUserName;
                            }
                        }
                        else
                        {
                            var updatable = entity as IModifieable;
                            if (updatable != null)
                            {
                                updatable.ModifiedOn = now;
                                updatable.ModifiedBy = contextData.CurrentUserName;
                            }
                        }                        
                    }
                }
            }
        }
    }
}

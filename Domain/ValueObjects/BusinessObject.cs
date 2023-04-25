using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Domain.Enums;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Classe usada para transitar dados das Views para controllers e virce versa
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessObject<T>
    {
        public BusinessObject()
        {
            Messages = new List<MessageResult>();
        }

        public BusinessObject(T data)
        {
            Data = data;
            Messages = new List<MessageResult>();
        }

        public BusinessObject(ClaimsIdentity identity, T data)
        {
            Data = data;
            Identity = identity;
            Messages = new List<MessageResult>();
        }

        public T Data { get; set; }
        public List<MessageResult> Messages { get; set; }
        public ClaimsIdentity Identity { get; set; }

        public bool IsValid
        {
            get
            {
                return !Messages.Any(x => x.Type == MessageType.Error);
            }
        }

        #region Factory

        public static BusinessObject<T> CreateValidResult(T model)
        {
            return new BusinessObject<T>(model);
        }

        public static BusinessObject<T> CreateValidResult(T model, string message)
        {
            var bo = new BusinessObject<T>(model);
            bo.WithSucess(message);
            return bo;
        }

        public static BusinessObject<T> CreateValidResult()
        {
            return new BusinessObject<T>(default);
        }

        public static BusinessObject<T> CreateInvalidResult(T model, string message)
        {
            var result = new BusinessObject<T>
            {
                Data = model,
                Messages = AddError(message)
            };
            return result;
        }

        public static BusinessObject<T> CreateInvalidResult(T model, List<MessageResult> message)
        {
            var result = new BusinessObject<T>
            {
                Data = model,
                Messages = message
            };
            return result;
        }

        public static BusinessObject<T> CreateInvalidResult(List<MessageResult> message)
        {
            var bo = new BusinessObject<T>
            {
                Messages = message
            };
            return bo;
        }

        public static BusinessObject<T> CreateInvalidResult(T model, string message, Exception exception = null)
        {
            var bo = new BusinessObject<T>
            {
                Data = model,
                Messages = AddError(message, exception)
            };
            return bo;
        }

        public static BusinessObject<T> CreateInvalidResult(IEnumerable<string> errors)
        {
            var bo = new BusinessObject<T>
            {
                Messages = AddError(errors)
            };
            return bo;
        }

        public void WithErrors(params string[] errors)
        {
            if (errors != null && errors.Any())
            {
                Messages.AddRange(AddError(errors));
            }
        }

        public void WithErrors(List<MessageResult> errors)
        {
            if (errors != null && errors.Any())
            {
                Messages.AddRange(errors);
            }
        }

        public void WithErrors(Exception exception)
        {
            if (exception != null)
            {
                Messages.AddRange(AddError(exception.Message));
            }
        }

        public void WithSucess(params string[] message)
        {
            if (message != null && message.Any())
            {
                Messages.AddRange(AddSucess(message));
            }
        }

        public void WithSucess(string message)
        {
            if (message != null && message.Any())
            {
                Messages.AddRange(AddSucess(message));
            }
        }

        private static List<MessageResult> AddSucess(string message)
        {
            var messageResult = new List<MessageResult>();
            messageResult.Add(new MessageResult()
            {
                Key = Guid.NewGuid().ToString(),
                Message = message,
                Type = MessageType.Success
            });
            return messageResult;
        }

        private static List<MessageResult> AddSucess(IEnumerable<string> message)
        {
            var messageResult = new List<MessageResult>();
            foreach (var item in message)
            {
                messageResult.Add(new MessageResult()
                {
                    Key = Guid.NewGuid().ToString(),
                    Message = item,
                    Type = MessageType.Success
                });
            }
            return messageResult;
        }

        private static List<MessageResult> AddError(string message, Exception exception = null)
        {
            var messageResult = new List<MessageResult>();
            messageResult.Add(new MessageResult()
            {
                Key = Guid.NewGuid().ToString(),
                Message = message,
                Type = MessageType.Error
            });
            if (exception != null)
            {
                messageResult.Add(new MessageResult()
                {
                    Key = Guid.NewGuid().ToString(),
                    Message = exception.Message,
                    Type = MessageType.Error
                });
            }
            return messageResult;
        }

        private static List<MessageResult> AddError(IEnumerable<string> errors, Exception exception = null)
        {
            var messageResult = new List<MessageResult>();
            foreach (var item in errors)
            {
                messageResult.Add(new MessageResult()
                {
                    Key = Guid.NewGuid().ToString(),
                    Message = item,
                    Type = MessageType.Error
                });
            }

            if (exception != null)
            {
                messageResult.Add(new MessageResult()
                {
                    Key = Guid.NewGuid().ToString(),
                    Message = exception.Message,
                    Type = MessageType.Error
                });
            }
            return messageResult;
        }

        #endregion
    }
}

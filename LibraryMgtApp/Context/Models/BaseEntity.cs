using LibraryMgtApp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Context.Models
{

    public interface IEntity<T>
    {
        T Id { get; set; }
    }

    public abstract class BaseEntity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        private List<ValidationError> _validationErrors = new List<ValidationError>();

        [NotMapped]
        public List<ValidationError> ValidationErrors
        {
            get
            {
                if (_validationErrors == null)
                { _validationErrors = new List<ValidationError>(); }
                return _validationErrors;
            }
            set { _validationErrors = value; }
        }

        [NotMapped]
        public Boolean HasErrors
        {
            get
            {
                return ValidationErrors.Count > 0;
            }
        }

        public virtual List<ValidationError> Validate()
        {
            return _validationErrors;
        }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
        public Boolean IsDeleted { get; set; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GitHubSearch
{
    public class ValidationResultList : IValidationResultList
    {
        private IList<ValidationResult> list;

        public ValidationResultList(IList<ValidationResult> validationResults = null)
        {
            list = validationResults ?? new List<ValidationResult>();
        }

        public ValidationResult this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return list.IsReadOnly; }
        }

        public int IndexOf(ValidationResult item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, ValidationResult item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public void Add(ValidationResult item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(ValidationResult item)
        {
            return list.Contains(item);
        }

        public void CopyTo(ValidationResult[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(ValidationResult item)
        {
            return list.Remove(item);
        }

        public IEnumerator<ValidationResult> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, list.Select(x => x.ToString()).ToArray());
        }
    }
}
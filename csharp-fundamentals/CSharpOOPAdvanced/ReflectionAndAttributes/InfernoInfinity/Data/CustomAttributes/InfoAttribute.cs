using System;

namespace InfernoInfinity.Data.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class InfoAttribute : Attribute
    {
        private string author;
        private int revision;
        private string description;
        private string[] reviewers;

        public InfoAttribute()
        {
            this.author = "Pesho";
            this.revision = 3;
            this.description = "Used for C# OOP Advanced Course - Enumerations and Attributes.";
            this.reviewers = new string[] { "Pesho", "Svetlio" };
        }

        public string Author => this.author;
        public int Revision => this.revision;
        public string Description => this.description;
        public string[] Reviewers => this.reviewers;

    }
}

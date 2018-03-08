using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Entity
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }

        private int age;
        [DataMember]
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value < 0 || value > 150)
                {
                    MessageBox.Show("错误：年龄必须在0-150岁之间！");
                }
                age = value;
            }
        }
        [DataMember]
        public Guid ClassId { get; set; }
    }
}

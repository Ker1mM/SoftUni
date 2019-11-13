using _03BarracksFactory.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace P03_BarraksWars.Core
{
    public class Controller
    {
        private IRepository repo;
        private IUnitFactory factory;

        public Controller(IRepository repository, IUnitFactory unitFactory)
        {
            this.repo = repository;
            this.factory = unitFactory;
        }

        public IExecutable ExecuteCommand(string[] data)
        {

            string fullcomand = char.ToUpper(data[0][0]) + data[0].Substring(1) + "Command";
            Type type = Type.GetType("P03_BarraksWars.Core.Commands." + fullcomand);

            var obj = (IExecutable)Activator.CreateInstance(type, new object[] { data });


            this.InjectDependancies(obj);
            return obj;
        }

        private void InjectDependancies(IExecutable command)
        {
            Type injectionType = typeof(InjectAttribute);

            IEnumerable<FieldInfo> fieldsForInjection = command.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(f => f.GetCustomAttributes().Any(a => a.GetType() == injectionType));

            IEnumerable<FieldInfo> interpreterFields = this.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (FieldInfo fieldForInjection in fieldsForInjection)
            {
                fieldForInjection.SetValue(command, interpreterFields
                    .First(f => f.FieldType == fieldForInjection.FieldType)
                    .GetValue(this));
            }
        }
    }
}

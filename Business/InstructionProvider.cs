﻿using NamNamAPI.Domain;
using NamNamAPI.Models;
using Newtonsoft.utility;
using System.Globalization;

namespace NamNamAPI.Business
{
    public class InstructionProvider
    {
        private NamnamContext connectionModel;

        public InstructionProvider(NamnamContext _connectionModel)
        {
            string Culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Culture);
            connectionModel = _connectionModel;
        }

          public int PostInstruction(List<CookinginstructionDomain> newInstructions,string idRecipe)
        {
            int changes = 0;
            int steps = 1;
            try{
                List<Cookinginstruction> instructionsTemp = new List<Cookinginstruction>();
                foreach(var item in newInstructions){

                    Cookinginstruction itemBD = new Cookinginstruction{
                        IdCookingInstruction = GenerateRandomID.GenerateID(),
                        Instruction = item.Instruction,
                        Step = steps,
                        RecipeIdRecipe = idRecipe
                    };
                    instructionsTemp.Add(itemBD);
                    steps++;
                }
                connectionModel.Cookinginstructions.AddRange(instructionsTemp);
                 changes = connectionModel.SaveChanges();

            }catch(Exception e){
                changes = 500;
            }
            return changes;
        }

        public bool UpdateInstruction(List<CookinginstructionDomain> newInstructions,string idRecipe)
        {
            bool changes = false;
            try{
                List<Cookinginstruction> instructionsTemp = new List<Cookinginstruction>();
                foreach(var item in newInstructions){

                    Cookinginstruction itemBD = new Cookinginstruction{
                        IdCookingInstruction = GenerateRandomID.GenerateID(),
                        Instruction = item.Instruction,
                        Step = item.Step,
                        RecipeIdRecipe = idRecipe
                    };
                    instructionsTemp.Add(itemBD);
                }
                connectionModel.Cookinginstructions.RemoveRange(connectionModel.Cookinginstructions.Where(a => a.RecipeIdRecipe == idRecipe));
                connectionModel.Cookinginstructions.AddRange(instructionsTemp);
                connectionModel.SaveChanges();
                changes = true;
            }catch(Exception){
                changes = false;
            }
            return changes;
        }
    }
}

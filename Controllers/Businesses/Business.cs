using KelimeDefteri.Models;
using KelimeDefteri.ViewModels.Defter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KelimeDefteri.Controllers.Businesses
{
    public class Business
    {
        private readonly DefterDB context;
        public Business(DefterDB context)
        {
            this.context = context;
        }
        public async Task<List<Record>> getAllRecords()
        {
            return await context.Records.Include(gk => gk.Words).ThenInclude(K => K.Definitions).ToListAsync();
        }


        public Word PrepareAWord(CreateRecordViewModel VM, int i)
        {
            Word word = new();
            word.Name = VM.GetType()?.GetProperty($"WordName{i}")?.GetValue(VM)?.ToString()?.makeFirstCapitilized();
            word.Definitions.Add                                //adding required definition and type inputs to its word.
            (
                new Definition
                {
                    definition = VM.GetType().GetProperty($"Definition{i}")?.GetValue(VM)?.ToString()?.makeFirstCapitilized(),
                    definitionType = VM.GetType().GetProperty($"Type{i}")?.GetValue(VM)?.ToString()?.makeFirstCapitilized()
                }
            );
            return word;
        }

        public List<string>? PrepareExtraDefinitions(NewRecordViewModel newInputs, int i)
        {
            return newInputs.GetType().GetProperty($"newDefinition{i}")?.GetValue(newInputs) as List<string>;
        }

        public List<string>? PrepareExtraTypes(NewRecordViewModel newInputs, int i)
        {
            return newInputs.GetType().GetProperty($"newType{i}")?.GetValue(newInputs) as List<string>;
        }

        public Word AddExtraDefinitions(Word word, List<string> newDefs, List<string> newTypes)
        {
            for (int d = 0; d < newDefs.Count; d++)     // add new definition objects by newDefs count.
                word.Definitions.Add(new Definition { definition = newDefs[d], definitionType = newTypes[d] });
            return word;
        }
    }
}

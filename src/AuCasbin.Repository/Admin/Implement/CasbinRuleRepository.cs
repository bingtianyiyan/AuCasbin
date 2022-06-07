using AuCasbin.Core.Db;
using AuCasbin.Core.Repositories;
using AuCasbin.Domain;
using NetCasbin.Model;
using NetCasbin.Persist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuCasbin.Repository.Admin.Implement
{
   public class CasbinRuleRepository : RepositoryBase<TCasbinRule>, ICasbinRuleRepository
    {
        public CasbinRuleRepository(DbUnitOfWorkManager uowm) : base(uowm)
        {
        }

        #region sync operations

        public void LoadPolicy(Model model)
        {
            var rules = Select.ToList(); 
            LoadPolicyData(model, Helper.LoadPolicyLine, rules);
        }

        public void RemovePolicy(string sec, string P_Type, IList<string> rule)
        {
            RemoveFilteredPolicy(sec, P_Type, 0, rule.ToArray());
        }

        public void RemoveFilteredPolicy(string sec, string P_Type, int fieldIndex, params string[] fieldValues)
        {
            if (fieldValues == null || !fieldValues.Any())
                return;
            var line = SavePolicyLine(sec, P_Type, fieldIndex, fieldValues);

            var query = Select.Where(x => x.FPtype == line.FPtype);
            query = ApplyQueryFilter(query, line);

            query.ToDelete().ExecuteAffrows();
        }

        public void SavePolicy(Model model)
        {
            var lines = SavePolicyLines(model);
            if (lines.Any())
            {
                Insert(lines);
                //_context.TCasbinRule.AddRange(lines);
                //_context.SaveChanges();
            }
        }

        public void AddPolicy(string sec, string P_Type, IList<string> rule)
        {
            var line = SavePolicyLine(P_Type, rule);
            Insert(line);
        }

        #endregion

        #region async operations

        public async Task LoadPolicyAsync(Model model)
        {
            var rules = await Select.ToListAsync();//await _context.TCasbinRule.AsNoTracking().ToListAsync();
            LoadPolicyData(model, Helper.LoadPolicyLine, rules);
        }

        public async Task SavePolicyAsync(Model model)
        {
            var lines = SavePolicyLines(model);
            if (lines.Any())
            {
                await InsertAsync(lines);
            }
        }

        public async Task AddPolicyAsync(string sec, string P_Type, IList<string> rule)
        {
            var line = SavePolicyLine(P_Type, rule);
            await InsertAsync(line);
        }

        public async Task RemovePolicyAsync(string sec, string P_Type, IList<string> rule)
        {
            await RemoveFilteredPolicyAsync(sec, P_Type, 0, rule.ToArray());
        }

        public async Task RemoveFilteredPolicyAsync(string sec, string P_Type, int fieldIndex, params string[] fieldValues)
        {
            if (fieldValues == null || !fieldValues.Any())
                return;
            var line = SavePolicyLine(sec, P_Type, fieldIndex, fieldValues);

            var query = Select.Where(x => x.FPtype == line.FPtype);
            query = ApplyQueryFilter(query, line);

             query.ToDelete().ExecuteAffrows();
            await Task.CompletedTask;
        }
        #endregion

        #region helper functions

        private void LoadPolicyData(Model model, Helper.LoadPolicyLineHandler<string, Model> handler, IEnumerable<TCasbinRule> rules)
        {
            foreach (var rule in rules)
            {
                handler(GetPolicyContent(rule), model);
            }
        }

        private string GetPolicyContent(TCasbinRule rule)
        {
            StringBuilder sb = new StringBuilder(rule.FPtype);
            void Append(string v)
            {
                if (string.IsNullOrEmpty(v))
                {
                    return;
                }
                sb.Append($", {v}");
            }
            Append(rule.FV0);
            Append(rule.FV1);
            Append(rule.FV2);
            Append(rule.FV3);
            Append(rule.FV4);
            Append(rule.FV5);
            return sb.ToString();
        }

        private List<TCasbinRule> SavePolicyLines(Model model)
        {
            List<TCasbinRule> lines = new List<TCasbinRule>();
            if (model.Model.ContainsKey("p"))
            {
                foreach (var kv in model.Model["p"])
                {
                    var P_Type = kv.Key;
                    var ast = kv.Value;
                    foreach (var rule in ast.Policy)
                    {
                        var line = SavePolicyLine(P_Type, rule);
                        lines.Add(line);
                    }
                }
            }
            if (model.Model.ContainsKey("g"))
            {
                foreach (var kv in model.Model["g"])
                {
                    var P_Type = kv.Key;
                    var ast = kv.Value;
                    foreach (var rule in ast.Policy)
                    {
                        var line = SavePolicyLine(P_Type, rule);
                        lines.Add(line);
                    }
                }
            }
            return lines;
        }
        private TCasbinRule SavePolicyLine(string P_Type, IList<string> rule)
        {
            var line = new TCasbinRule();
            line.FPtype = P_Type;
            if (rule.Count() > 0)
            {
                line.FV0 = rule[0];
            }
            if (rule.Count() > 1)
            {
                line.FV1 = rule[1];
            }
            if (rule.Count() > 2)
            {
                line.FV2 = rule[2];
            }
            if (rule.Count() > 3)
            {
                line.FV3 = rule[3];
            }
            if (rule.Count() > 4)
            {
                line.FV4 = rule[4];
            }
            if (rule.Count() > 5)
            {
                line.FV5 = rule[5];
            }

            return line;
        }

        private TCasbinRule SavePolicyLine(string sec, string P_Type, int fieldIndex, params string[] fieldValues)
        {
            var line = new TCasbinRule()
            {
                FPtype = P_Type
            };
            var len = fieldValues.Count();
            if (fieldIndex <= 0 && 0 < fieldIndex + len)
            {
                line.FV0 = fieldValues[0 - fieldIndex];
            }
            if (fieldIndex <= 1 && 1 < fieldIndex + len)
            {
                line.FV1 = fieldValues[1 - fieldIndex];
            }
            if (fieldIndex <= 2 && 2 < fieldIndex + len)
            {
                line.FV2 = fieldValues[2 - fieldIndex];
            }
            if (fieldIndex <= 3 && 3 < fieldIndex + len)
            {
                line.FV3 = fieldValues[3 - fieldIndex];
            }
            if (fieldIndex <= 4 && 4 < fieldIndex + len)
            {
                line.FV4 = fieldValues[4 - fieldIndex];
            }
            if (fieldIndex <= 5 && 5 < fieldIndex + len)
            {
                line.FV5 = fieldValues[5 - fieldIndex];
            }
            return line;
        }

        private FreeSql.ISelect<TCasbinRule> ApplyQueryFilter(FreeSql.ISelect<TCasbinRule> query, TCasbinRule line)
        {
            if (!string.IsNullOrEmpty(line.FV0))
            {
                query = query.Where(p => p.FV0 == line.FV0);
            }
            if (!string.IsNullOrEmpty(line.FV1))
            {
                query = query.Where(p => p.FV1 == line.FV1);
            }
            if (!string.IsNullOrEmpty(line.FV2))
            {
                query = query.Where(p => p.FV2 == line.FV2);
            }
            if (!string.IsNullOrEmpty(line.FV3))
            {
                query = query.Where(p => p.FV3 == line.FV3);
            }
            if (!string.IsNullOrEmpty(line.FV4))
            {
                query = query.Where(p => p.FV4 == line.FV4);
            }
            if (!string.IsNullOrEmpty(line.FV5))
            {
                query = query.Where(p => p.FV5 == line.FV5);
            }
            return query;
        }


        #endregion

        public void AddPolicies(string sec, string ptype, IEnumerable<IList<string>> rules)
        {
            List<TCasbinRule> list = new List<TCasbinRule>();
            foreach (var rule in rules)
            {
                var line = SavePolicyLine(ptype, rule);
                list.Add(line);
            }
             Insert(list);
        }

        public async Task AddPoliciesAsync(string sec, string ptype, IEnumerable<IList<string>> rules)
        {
            List<TCasbinRule> list = new List<TCasbinRule>();
            foreach (var rule in rules)
            {
                var line = SavePolicyLine(ptype, rule);
                list.Add(line);
            }
           await InsertAsync(list);
        }

        public void RemovePolicies(string sec, string ptype, IEnumerable<IList<string>> rules)
        {
            foreach(var rule in rules)
            {
                RemoveFilteredPolicy(sec, ptype, 0, rule.ToArray());
            }
        }

        public async Task RemovePoliciesAsync(string sec, string ptype, IEnumerable<IList<string>> rules)
        {
            foreach (var rule in rules)
            {
               await RemoveFilteredPolicyAsync(sec, ptype, 0, rule.ToArray());
            }
        }
    }
}

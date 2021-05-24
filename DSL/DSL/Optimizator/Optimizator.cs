using System;
using System.Collections.Generic;
using System.Text;

namespace DSL
{
    public class Optimizator
    {
        public List<AbstractExpression> exprList;

        public Optimizator(List<AbstractExpression> expressions)
        {
            exprList = expressions;
        }

        public void Optimize()
        {
            PeepholeOptimize();
        }

        private void PeepholeOptimize()
        {
            for(int i=0; i<exprList.Count; i++)
            {
                if (exprList[i] is InitializationExpression)
                {
                    var initExpr = (InitializationExpression)exprList[i];
                    var assgnExprIndex = findAssignmentExprIndex(i, initExpr);
                    var assgnExpr = (AssignmentExpression)exprList[assgnExprIndex];

                    if (assgnExpr != null && assgnExpr.propertiesExpression == null)
                    {
                        var declExpr = createDeclExpr(initExpr, assgnExpr);
                        exprList[i] = declExpr;
                        exprList.RemoveAt(assgnExprIndex);
                        PeepholeOptimize();
                    }
                }
            }
               
        }

        private DeclarationExpression createDeclExpr(InitializationExpression initExpr, AssignmentExpression assgnExpr)
        {
            DeclarationExpression declExpr = new DeclarationExpression();
            declExpr.initializationExpression = initExpr;
            declExpr.expression = assgnExpr.Right;

            return declExpr;
        }

        private int findAssignmentExprIndex(int startPointer, InitializationExpression initExpr)
        {
            for(int i = startPointer; i<exprList.Count; i++)
            {
                if(exprList[i] is AssignmentExpression 
                    && initExpr.identifier == ((AssignmentExpression)exprList[i]).identifier)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

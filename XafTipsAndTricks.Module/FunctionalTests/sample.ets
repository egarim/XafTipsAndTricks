;#DropDB XafTipsAndTricksEasyTest

#Application XafTipsAndTricksWin
#Application XafTipsAndTricksBlazor

*FillForm
 User Name = Admin
*Action Log In

#IfDef XafTipsAndTricksWin

*Action About...
 
*Action OK

#EndIf

#IfDef XafTipsAndTricksBlazor
*Action Navigation(My Details)
#EndIf

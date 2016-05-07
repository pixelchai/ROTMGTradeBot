cd "$(SolutionDir)"
for /f %%x in (buildver.txt) do (
set /a var=%%x+1
)
>buildver.txt echo %var%
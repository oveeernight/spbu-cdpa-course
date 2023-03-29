# DPLL SAT Solver

This solution contains SAT solver based on [DPLL algorithm](https://en.wikipedia.org/wiki/DPLL_algorithm). 
Solver accepts path to file in [DIMACS format](https://www.cs.utexas.edu/users/moore/acl2/manuals/current/manual/index-seo.php/SATLINK____DIMACS) as 
command line argument.
## Usage
Ensure you have [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed.
### Build
````console 
git clone https://github.com/oveeernight/spbu-cdpa-course
cd spbu-cdpa-course/Homework3/DPLL/DPLL
dotnet publish "DPLL.csproj" -c Release -o /path/to/build/app
cd /path/to/build/app
````
### Run
Windows 
````console
DPLL.exe /path/to/file
- ````
Linux
````console 
./DPLL /path/to/file
````

Example output: 
````console 
SAT
-138 149 43 -172 -54 -67 -79 182 -23 -40 106 -134 161 -10 152 156 110 119 -73 147 109 
96 -190 18 -132 197 148 -100 -170 105 196 -151 -25 50 80 129 21 -84 59 -48 -57 -3 9 20
-12 174 61 179 -130 -133 60 70 146 -142 167 -108 -91 -81 -104 -28 171 -101 41 192 -68 
155 198 -94 143 51 -82 -141 64 127 -166 -145 -38 -39 131 -124 -121 -157 -56 -118 137 
144 -35 -49 -95 188 189 114 -111 76 30 176 2 -46 200 -34 117 -181 107 33 24 -62 191 8 
-63 -195 74 -87 -71 -115 158 -102 -66 -199 -7 -140 -135 -98 153 -160 -65 -120 77 5 69 
83 -15 159 165 -31 -187 -44 -88 -183 123 6 -11 13 -194 -53 58 112 -45 -169 184 26 -163 
72 -99 -103 173 29 154 -186 -168 -164 -19 42 -75 -85 89 -185 162 139 122 136 14 -22 27
113 37 47 -116 -32 -1 -150 -128 178 97 17 -52 16 78 -36 -86 -4 92 126 93 -175 -180 -90
-193 -55 177 -125
````


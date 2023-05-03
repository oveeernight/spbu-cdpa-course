import numpy as np
from scipy import stats
import matplotlib.pyplot as plt

with open("measurements.txt", "r") as file:
    values = [float(x) for x in file]
    print(values)
    plt.hist(values, color='lightblue')
    plt.show()


    (sta, pvalue_n) = stats.normaltest(values)
    (stb, pvalue_s) = stats.shapiro(values)
    print("pvalue normaltest", pvalue_n)
    print("pvalue shapiro", pvalue_s)
    
    
    print("mean", np.mean(values))
    print("deviation", np.std(values, ddof=1))

    print(stats.t.ppf(0.975, df=len(values)-1)*stats.sem(values))




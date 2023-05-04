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
    print("pvalue normaltest:", pvalue_n)
    print("pvalue shapiro:", pvalue_s)
    
    mean = np.mean(values)
    print("mean:", mean)
    std = np.std(values, ddof=1)
    print("deviation:", std)

    msd = std/np.sqrt(40)
    print("msd:", msd)

    print("доверительный:", mean, "+-", 2 * msd)
    print("предсказывающий:", mean, "+-", 2 * std)





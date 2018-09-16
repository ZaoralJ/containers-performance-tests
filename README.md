# Singleton, one conatiner
- MS container static 1x times in 11 ms
- MS container static 100x times in 3 ms
- MS container static 1000x times in 0 ms
- MS container static 100000x times in 5 ms
- MS container static 1000000x times in 45 ms
- Castle.Windsor static 1x times in 24 ms
- Castle.Windsor static 100x times in 0 ms
- Castle.Windsor static 1000x times in 0 ms
- Castle.Windsor static 100000x times in 24 ms
- Castle.Windsor static 1000000x times in 238 ms

# Transient, one conatiner
- MS container static 1x times in 0 ms
- MS container static 100x times in 0 ms
- MS container static 1000x times in 0 ms
- MS container static 100000x times in 8 ms
- MS container static 1000000x times in 52 ms
- Castle.Windsor static 1x times in 0 ms
- Castle.Windsor static 100x times in 0 ms
- Castle.Windsor static 1000x times in 1 ms
- Castle.Windsor static 100000x times in 142 ms
- Castle.Windsor static 1000000x times in 1433 ms

# Complex transient, one conatiner
- MS container static 1x times in 0 ms
- MS container static 100x times in 0 ms
- MS container static 1000x times in 0 ms
- MS container static 100000x times in 5 ms
- MS container static 1000000x times in 54 ms
- Castle.Windsor static 1x times in 0 ms
- Castle.Windsor static 100x times in 0 ms
- Castle.Windsor static 1000x times in 1 ms
- Castle.Windsor static 100000x times in 145 ms
- Castle.Windsor static 1000000x times in 1447 ms

# Complex transient, new container for each iteration
- MS container static 1x times in 0 ms
- MS container static 100x times in 0 ms
- MS container static 1000x times in 2 ms
- MS container static 100000x times in 219 ms
- Castle.Windsor static 1x times in 0 ms
- Castle.Windsor static 100x times in 5 ms
- Castle.Windsor static 1000x times in 51 ms
- Castle.Windsor static 100000x times in 5153 ms

main: sequential.o gsl.o parallel.o main.o
	gcc -O3 -fopenmp -o $@ $^ -lgsl -lgslcblas -lm

parallel.o: parallel.c
	gcc -c -O3 -fopenmp -o $@ $<

gsl.o: gsl.c
	gcc -c -O3 -o $@ $< -lgsl -lgslcblas -lm

%.o: %.c
	gcc -c -O3 -o $@ $< 

.PHONY = clean

clean :
	rm -f *.o main
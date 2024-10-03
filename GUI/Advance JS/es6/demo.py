import random

# Fitness function to calculate the total value and total weight of a solution
def fitness(solution, weights, values, capacity):
    total_weight = total_value = 0
    for i in range(len(solution)):
        if solution[i] == 1:
            total_weight += weights[i]
            total_value += values[i]
    if total_weight > capacity:
        return 0  # Invalid solution due to exceeding capacity
    return total_value

# Create an initial population of random solutions
def create_population(pop_size, num_items):
    return [[random.randint(0, 1) for _ in range(num_items)] for _ in range(pop_size)]

# Selection based on fitness (higher value solutions are more likely to be selected)
def selection(population, weights, values, capacity):
    fitnesses = [fitness(individual, weights, values, capacity) for individual in population]
    return random.choices(population, weights=fitnesses, k=len(population))

# Crossover between two individuals to produce offspring
def crossover(parent1, parent2):
    crossover_point = random.randint(1, len(parent1) - 1)
    return parent1[:crossover_point] + parent2[crossover_point:]

# Mutation to introduce random variations
def mutate(individual, mutation_rate):
    for i in range(len(individual)):
        if random.random() < mutation_rate:
            individual[i] = 1 - individual[i]  # Flip the bit

# Genetic Algorithm to solve the Knapsack problem
def genetic_algorithm(weights, values, capacity, pop_size=100, generations=100, mutation_rate=0.01):
    population = create_population(pop_size, len(weights))
    
    for generation in range(generations):
        population = selection(population, weights, values, capacity)
        new_population = []
        
        # Apply crossover and mutation to generate new population
        for i in range(0, len(population), 2):
            parent1 = population[i]
            parent2 = population[i + 1] if i + 1 < len(population) else population[0]
            child1 = crossover(parent1, parent2)
            child2 = crossover(parent2, parent1)
            mutate(child1, mutation_rate)
            mutate(child2, mutation_rate)
            new_population.extend([child1, child2])
        
        population = new_population

    # Return the best solution from the final population
    best_solution = max(population, key=lambda individual: fitness(individual, weights, values, capacity))
    return best_solution, fitness(best_solution, weights, values, capacity)

# Example usage
weights = [10, 20, 30, 40, 50]
values = [60, 100, 120, 240, 150]
capacity = 100

solution, max_value = genetic_algorithm(weights, values, capacity)
print("Best solution:", solution)
print("Maximum value:", max_value)

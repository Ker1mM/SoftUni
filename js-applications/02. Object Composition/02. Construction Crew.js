function solve(worker) {
    if (worker.hasOwnProperty('dizziness') && worker.dizziness) {
        let hydratation = 0.1 * worker.weight * worker.experience;
        worker.levelOfHydrated += hydratation;
        worker.dizziness = false;
        return worker;
    }

    return worker;
};

console.log(solve(
    { weight: 95,
        experience: 3,
        levelOfHydrated: 0,
        dizziness: false }
      
      
));
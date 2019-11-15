function autoEngineeringCompany(input) {
    let producedCars = [];

    for (const vehicle of input) {
        let carInfo = vehicle.split(' | ');
        let carBrand = carInfo[0];
        let carModel = carInfo[1];
        let carCount = Number(carInfo[2]);

        if (producedCars[carBrand] !== undefined) {
            if (producedCars[carBrand].models[carModel] !== undefined) {
                producedCars[carBrand].models[carModel] += carCount;
            } else {
                producedCars[carBrand].models[carModel] = carCount;
            }
        } else {
            producedCars[carBrand] = {};
            producedCars[carBrand].models = [];
            producedCars[carBrand].models[carModel] = carCount;
        }
    }

    for (const vehicle in producedCars) {
        console.log(vehicle);
        for (const car in producedCars[vehicle].models) {
            console.log(`###${car} -> ${producedCars[vehicle].models[car]}`);
        }
    }
}
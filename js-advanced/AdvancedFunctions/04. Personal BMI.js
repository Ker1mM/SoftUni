function personalBMI(name, age, weight, height) {
    let BMI = Math.round(weight / (height / 100) / (height / 100));

    let result = {
        name,
        personalInfo: {
            age: age,
            weight: weight,
            height: height
        },
        BMI
    }

    let status = 'obese';
    if (result.BMI < 18.5) {
        status = 'underweight';
    } else if (result.BMI < 25) {
        status = 'normal';
    } else if (result.BMI < 30) {
        status = 'overweight';
    }

    result.status = status;
    if (result.BMI >= 30) {
        result.recommendation = 'admission required';
    }

    return result;
}
class Company {
    constructor() {
        this.departments = [];
        this.salaries = {};
    }

    addEmployee(username, salary, position, department) {
        if (!username || !salary || !position || !department) {
            throw Error('Invalid input!');
        }

        if (salary < 0) {
            throw Error(' Invalid input!');
        }

        let employee = { username, salary, position, department };
        this.departments.push(employee);

        if (!this.salaries.hasOwnProperty(department)) {
            this.salaries[department] = [];
        }

        this.salaries[department].push(salary);

        return `New employee is hired. Name: ${username}. Position: ${position}`;
    }

    bestDepartment() {
        let bestDepartmentName = '';
        let bestDepartmentAvgSalary = 0;

        for (let dep of Object.keys(this.salaries)) {
            let currentAvg = this.salaries[dep].reduce((a, b) => a + b, 0) / this.salaries[dep].length;
            if (currentAvg >= bestDepartmentAvgSalary) {
                bestDepartmentAvgSalary = currentAvg;
                bestDepartmentName = dep;
            }
        }

        let result = [];
        result.push(`Best Department is: ${bestDepartmentName}`);
        result.push(`Average salary: ${bestDepartmentAvgSalary.toFixed(2)}`);

        let employees = this.departments.filter(x => x.department === bestDepartmentName);
        employees.sort((a, b) => {
            if (a.salary > b.salary) {
                return -1;
            } else if (a.salary < b.salary) {
                return 1;
            } else {
                if (a.username > b.username) {
                    return 1;
                } else if (a.username < b.username) {
                    return -1;
                } else {
                    return 0;
                }
            }
        })
        for (let dep of employees) {
            result.push(`${dep.username} ${dep.salary} ${dep.position}`);
        }

        return result.join('\n');
    }
}

// let c = new Company();
// c.addEmployee("Stanimir", 2000, "engineer", "Construction");
// c.addEmployee("Pesho", 1500, "electrical engineer", "Construction");
// c.addEmployee("Slavi", 500, "dyer", "Construction");
// c.addEmployee("Stan", 2000, "architect", "Construction");
// c.addEmployee("Stanimir", 1200, "digital marketing manager", "Marketing");
// c.addEmployee("Pesho", 1000, "graphical designer", "Marketing");
// c.addEmployee("Gosho", 1350, "HR", "Human resources");
// console.log(c.bestDepartment());

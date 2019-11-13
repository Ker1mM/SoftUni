function solve() {
    document.body.innerHTML = `<div id="content"></div>`;
    let result = {
        id: 0,
        content: document.querySelector('#content'),
        reports: [],
        sortOrder: 'ID',
        report(author, description, reproducible, severity) {
            let newReport = {
                ID: this.id++,
                author,
                description,
                reproducible,
                severity,
                status: 'Open'
            }

            this.reports.push(newReport);
            this.display();
        },

        setStatus(id, newStatus) {
            let oldReport = this.reports.find(x => x.ID === id);
            if (oldReport) {
                let index = this.reports.indexOf(oldReport);
                this.reports[index].status = newStatus;
                this.display();
            }
        },
        remove(id) {
            let oldReport = this.reports.find(x => x.ID === id);
            if (oldReport) {
                let index = this.reports.indexOf(oldReport);
                this.reports.splice(index, 1);
                this.display();
            }
        },
        display() {
            this.content.innerHTML = '';
            this.reports = this.reports.sort((a, b) => {
                if (a[this.sortOrder] < b[this.sortOrder]) { return -1 };
                if (a[this.sortOrder] > b[this.sortOrder]) { return 1 };
                return 0;
            });
            for (let rep of this.reports) {
                let mainBody = document.createElement('div');
                mainBody.setAttribute('id', `report_${rep.ID}`);
                mainBody.setAttribute('class', 'report');

                let body = document.createElement('div');
                body.setAttribute('class', 'body');
                let desc = document.createElement('p');
                desc.innerHTML = rep.description;
                body.appendChild(desc);
                mainBody.appendChild(body);

                let title = document.createElement('div');
                title.setAttribute('class', 'title');

                let auth = document.createElement('span');
                auth.setAttribute('class', 'author');
                auth.innerHTML = `Submitted by: ${rep.author}`;
                title.appendChild(auth);

                let status = document.createElement('span');
                status.setAttribute('class', 'status');
                status.innerHTML = `${rep.status} | ${rep.severity}`;
                title.appendChild(status);

                mainBody.appendChild(title);
                this.content.appendChild(mainBody);
            }
        },
        sort(method) {
            this.sortOrder = method;
            this.display();
        },
        output(selector) {
            this.content = document.querySelector(selector);
        }
    }
    return result;
}
solve();
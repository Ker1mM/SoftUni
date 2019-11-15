function JSONTable(input) {
    let result = ["<table>"]

    for (const row of input) {
        let arr = JSON.parse(row);
        result.push('<tr>');
        addRow(arr);
        result.push('</tr>');
    }
    result.push('</table>');

    function addRow(row) {
        for (const value of Object.values(row)) {
           let resultRow = escapeHtml(String(value));
           result.push(`<td>${resultRow}</td>`)
        }
    }

    console.log(result.join('\n'));

    function escapeHtml(unsafe) {
        return unsafe
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;");
    }
}
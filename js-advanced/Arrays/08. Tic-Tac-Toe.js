function solve(input) {
    let turns = input;
    let spaceLeft = 9;
    let mark = 'X';
    let board = [[false, false, false],
    [false, false, false],
    [false, false, false]];

    for (const turn of turns) {
        let tokens = turn.split(' ');
        let row = Number(tokens[0]);
        let col = Number(tokens[1]);
        let hasWinner = false;
        let movePossible = canPlace(row, col);

        if (movePossible) {
            board[row][col] = mark;

            if (spaceLeft <= 5 && checkWinner(row, col)) {
                console.log(`Player ${mark} wins!`);
                printBoard();
                break;
            }

            mark = mark === 'X' ? 'O' : 'X';
            spaceLeft--;
        }

        if (spaceLeft == 0) {
            console.log("The game ended! Nobody wins :(");
            printBoard();
            break;
        }
    }

    function printBoard() {
        board.forEach(element => {
            console.log(element.join('\t'));
        });
    }

    function checkWinner(row, col) {
        return checkRow(row) || checkCol(col) || checkDiagonal(row, col);
    }

    function checkDiagonal(row, col) {

        let leftDia = true;
        let rightDia = true;

        for (let i = 0; i < 2; i++) {
            if (board[i][i] !== board[i + 1][i + 1]) {
                leftDia = false;
            }

            if (board[i][2 - i] !== board[i + 1][1 - i]) {
                rightDia = false;
            }
        }

        return leftDia || rightDia;
    }

    function checkRow(row) {
        for (let i = 0; i < 2; i++) {
            if (board[row][i] !== board[row][i + 1]) {
                return false;
            }
        }

        return true;
    }

    function checkCol(col) {
        for (let i = 0; i < 2; i++) {
            if (board[i][col] !== board[i + 1][col]) {
                return false;
            }
        }

        return true;
    }

    function canPlace(row, col) {
        if (board[row][col] === false) {
            return true;
        } else {
            console.log("This place is already taken. Please choose another!");
            return false;
        }
    }
}
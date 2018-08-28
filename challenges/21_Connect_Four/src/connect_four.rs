extern crate console;

use console::Term;
use console::Key;
use console::style;

fn main() {
    let term = Term::stdout();
    println!("<------------Connect four------------->");
    println!("Created by Morasiu (morasiu2@gmail.com)");
    println!("Press any key to start");
    term.read_key().ok();
    let mut player: i32 = 1;
    let mut cursor: i32 = 0;

    let mut board = [[0, 0, 0, 0, 0, 0, 0],
                     [0, 0, 0, 0, 0, 0, 0],
                     [0, 0, 0, 0, 0, 0, 0],
                     [0, 0, 0, 0, 0, 0, 0],
                     [0, 0, 0, 0, 0, 0, 0],
                     [0, 0, 0, 0, 0, 0, 0]];
    loop {
        print_screen(&player, &cursor, &board);
        handle_input(&term, &mut player, &mut cursor, &mut board);
    }
}

fn print_screen(player: &i32, cursor: &i32, board: &[[i32; 7]; 6]) {
    clear();
    println!("+----------------+");
    println!("|<(Connect four)>|");
    println!("+----------------+\n");
    println!("HELP: A - left, D - right, Space - put, Q - Quit");
    if *player == 1 { print!("{}\n", style("Player: ".to_string() + &player.to_string()).green());}
    else { print!("{}\n", style("Player: ".to_string() + &player.to_string()).red());}

    print_top_line();
    for i in 0..board.len(){
        print!("| ");
        for j in 0..board[i].len(){
            // match board[i][j] {
            //     0 => print!("{}{} ", i, j),
            //     1 => print!("{}{} ", style(i).green(), style(j).green()),
            //     2 => print!("{}{} ", style(i).red(), style(j).red()),
            //     _ => ()
            // }
            match board[i][j] {
                0 => print!("o "),
                1 => print!("{} ", style("o").green()),
                2 => print!("{} ", style("o").red()),
                _ => ()
            }
        }
        print!("|\n");
    }
    print_top_line();
    print!("  ");
    for _x in 0..*cursor {
        print!("  ");
    }
    println!("^");
}

fn handle_input(term: &console::Term, player: &mut i32, cursor: &mut i32, board: &mut [[i32; 7]; 6]) {
    let key = term.read_key().ok();
    match key.unwrap() {
        Key::Char('q') => std::process::exit(0),
        Key::Char(' ') => {
            for x in (0..board.len()).rev(){
                if board[x][*cursor as usize] == 0 { 
                    board[x][*cursor as usize] = *player;
                    check_for_win(x as i32, *cursor, *board);
                    if *player == 1 { *player = 2; }
                    else { *player = 1; }
                    break; 
                };                         
            }  
            check_draw(*board);
        },
        Key::Char('a') => if *cursor > 0 { *cursor += -1; },
        Key::Char('d') => if *cursor < 6 { *cursor += 1; },        
        _ => ()
    }
}

fn check_for_win(x: i32, y: i32, board: [[i32; 7]; 6]){
    let player = board[x as usize][y as usize];
    // Top left -1, -1
    if x - 1 >= 0 && y - 1 >= 0 && board[(x - 1) as usize][(y - 1) as usize] == player {
        // if 3rd is matching
        if x - 2 >= 0 && y - 2 >= 0 && board[(x - 2) as usize][(y - 2) as usize] == player {
            // if 4th is matching
            if x - 3 >= 0 && y - 3 >= 0 && board[(x - 3) as usize][(y - 3) as usize] == player {
                end_game(player, board);
            // if 4th is matching
            } else if x + 1 < board.len() as i32 && y + 1 < board[0].len() as i32 && board[(x + 1) as usize][(y + 1) as usize] == player {
                end_game(player, board);
            }
        } else if x + 1 < board.len() as i32 && y + 1 < board[0].len() as i32 && board[(x + 1) as usize][(y + 1) as usize] == player {
            if x + 2 < board.len() as i32 && y + 2 < board[0].len() as i32 && board[(x + 2) as usize][(y + 2) as usize] == player {
                end_game(player, board);
            }
        }
    }
    // Top right -1, +1
    else if x - 1 > 0 && y + 1 < board[0].len() as i32 && board[(x - 1) as usize][(y + 1) as usize] == player {
        if x - 2 >= 0 && y + 2 < board[0].len() as i32 && board[(x - 2) as usize][(y + 2) as usize] == player {
            if x - 3 >= 0 && y + 3 < board[0].len() as i32 && board[(x - 3) as usize][(y + 3) as usize] == player {
                end_game(player, board);
            } else if x + 1 > board.len() as i32  && y - 1 >= 0 && board[(x + 1) as usize][(y - 1) as usize] == player {
                end_game(player, board);
            }
        } else if x + 1 > board.len() as i32  && y - 1 >= 0 && board[(x + 1) as usize][(y - 1) as usize] == player {
            if x + 2 > board.len() as i32  && y - 2 >= 0 && board[(x + 2) as usize][(y - 2) as usize] == player {
                end_game(player, board);
            }
        }
     }
    // Center left 0, -1
    else if y - 1 >= 0 && board[x as usize][(y -1) as usize] == player {
        if y - 2 >= 0 && board[x as usize][(y - 2) as usize] == player {
            if y - 3 >= 0 && board[x as usize][(y - 3) as usize] == player {
                end_game(player, board);
            } else if y + 1 < board[x as usize].len() as i32 && board[x as usize][(y + 1) as usize] == player {
                end_game(player, board);
            }
        } else if  y + 1 < board[x as usize].len() as i32 && board[x as usize][(y + 1) as usize] == player {
            if y + 2 < board[x as usize].len() as i32 && board[x as usize][(y  + 2) as usize] == player {
                end_game(player, board);
            }
        }
     }
    // Center right 0, +1
    else if y + 1 < board[x as usize].len() as i32 && board[x as usize][(y + 1) as usize] == player {
        if y + 2 < board[x as usize].len() as i32 && board[x as usize][(y + 2) as usize] == player {
            if y + 3 < board[x as usize].len() as i32 && board[x as usize][(y + 3) as usize] == player {
                end_game(player, board);
            }
        }
    }
    // Bottom left +1, -1
    else if x + 1 < board.len() as i32 && y - 1 >= 0 &&  board[(x + 1) as usize][(y - 1) as usize] == player {
        if x + 2 < board.len() as i32 && y - 2 >= 0  && board[(x + 2) as usize][(y - 2) as usize] == player {
            if x + 3 < board.len() as i32 && y - 3 >= 0  && board[(x + 3) as usize][(y - 3) as usize] == player {
                end_game(player, board);           
            }
        }
     }
    // Bottom center +1, 0
    else if x + 1 < board.len() as i32 &&  board[(x + 1) as usize][y as usize] == player {
        if x + 2 < board.len() as i32 && board[(x + 2) as usize][y as usize] == player {
            if x + 3 < board.len() as i32 && board[(x + 3) as usize][y as usize] == player {
                end_game(player, board);
            }
        }
     }
    // Bottom right, +1, +1
        else if x + 1 < board.len() as i32 && y + 1 < board[0].len() as i32 && board[(x + 1) as usize][(y + 1) as usize] == player{
        if x + 2 < board.len() as i32 &&  y + 2 < board[0].len() as i32 && board[(x + 2) as usize][(y + 2) as usize] == player {
            if x + 3 < board.len() as i32 &&  y + 3 < board[0].len() as i32 && board[(x + 3) as usize][(y + 3) as usize] == player {
                end_game(player, board);           
            }
        }
     }
}

fn end_game(player: i32, board: [[i32; 7]; 6]){
    print_screen(&player, &0, &board);
    println!("Player {} won!", player);
    std::process::exit(0);
}

fn check_draw(board: [[i32; 7]; 6]) {
    let mut draw = true;
    for line in board.iter() {
        for field in line.iter() {
            if *field == 0 {
                draw = false;
            }
        }
    }
    if draw {
        println!("IT'S A DRAW");
        std::process::exit(0);
    }
}

fn print_top_line(){
    print!("+");
    for _x in 0..15 {
        print!("-");
    }
    print!("+\n");
}

fn clear(){
    // This is sequence, which should clears termianl.
    println!("{}[2J",27 as char);
}
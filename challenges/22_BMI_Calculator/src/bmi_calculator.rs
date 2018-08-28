extern crate console;

use console::Term;
use console::style;

fn main() {
    let term = Term::stdout();
    println!("<-----------BMI Calculator------------->");
    println!("Created by {}", style("Morasiu (morasiu2@gmail.com)").green());
    println!("Press any key to start");
    term.read_key().ok();

    let height: f64  = loop {
        print!("Enter your height (in meters, e.g. 1.76):\n");
        let input = term.read_line().unwrap();
        let parse = input.trim().parse::<f64>();
        let _result: f64 = match parse {
            Ok(num) => break num,
            Err(_) => { println!("Not a number"); continue } 
        };
    };
    let weight: f64  = loop {
        print!("Enter your weight (in kg, e.g. 55.2):\n");
        let input = term.read_line().unwrap();
        let parse = input.trim().parse::<f64>();
        let _result: f64 = match parse {
            Ok(num) => break num,
            Err(_) => { println!("Not a number"); continue } 
        };
    };

    let bmi: f64 = weight/(height * height);
    println!("Your BMI is: {:.2}", bmi);
    if bmi < 18.5 {
        println!("You are {}", style("underweight").red());
    } else if bmi > 25.0 {
        println!("You are {}", style("overweight").red());
    } else {
        println!("You are {}", style("in perfect shape, good job :)").green());
    }

    term.read_key().ok();
}
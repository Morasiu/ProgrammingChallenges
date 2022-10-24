namespace ProjectEuler; 

public class MathHelper {
	public static bool IsDivisible(int num){
		for(int i = 1; i <= 20; i++){
			if(num % i != 0){
				return false;
			}
		}
		return true;
	}
	
	public static bool IsPalindrome(int num){
		string sNum = num.ToString();
		for (int k = 0; k < sNum.Length/2; k++){
			if (sNum[k] != sNum[sNum.Length - 1 - k]){
				return false;
			} 
		}
		return true;
	} 
	
	public static bool IsPrime(int num){
		for (int i = 2; i < num/2; i++){
			if (num % i == 0)
				return false;
		}
		return true;
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallengeJustinSpaid
{
    class ReassembleFragments
    {
        static void Main(string[] args)
        {
            String[] testFragments = new String[4];
            testFragments[0] = "all is well";
            testFragments[1] = "ell that en";
            testFragments[2] = "hat end";
            testFragments[3] = "t ends well";

            // testFragments[0] = "O draconia";
            // testFragments[1] = "conian devil!";
            // testFragments[2] = "Oh la";
            // testFragments[3] = "h lame sa";
            // testFragments[4] = "saint!";

            String final = Reassemble(testFragments);
            Console.WriteLine(final);
        }

        public static String Reassemble(String[] fragments){
            if(fragments == null || fragments.Length == 0)
                return String.Empty;

            List<string> fragmentList = new List<string>(fragments);

            int poop = 10;
            while (fragmentList.Count > 1 && poop >= 0) {
                poop--;
                fragmentList = CalulateMergedStrings(fragmentList);
            }

            return fragmentList[0];
        }

        private static List<string> CalulateMergedStrings(List<string> fragments){

            fragments.Remove(string.Empty);

            int maxOverlap = 0;
            String string1 = String.Empty;
            String string2 = String.Empty;
            String mergedString = string.Empty;

            // Calculate largest mergedString and remove s1 and s2 from fragments 
            // and append mergedString to fragments
            for(int i = 0; i < fragments.Count; i++) {
                for (int j = i + 1; j < fragments.Count; j++) {
                    int currOverlap = CalculateOverlap(fragments.ElementAt(i), fragments.ElementAt(j));

                    if(currOverlap > maxOverlap){

                        String tmpString = MergeStrings(fragments.ElementAt(i), fragments.ElementAt(j));

                        if(tmpString != String.Empty){
                            string1 = fragments.ElementAt(i);
                            string2 = fragments.ElementAt(j);
                            maxOverlap = currOverlap;
                            mergedString = tmpString;
                        }
                    }

                    if(currOverlap == 0 && fragments.Count == 2){
                        string1 = fragments.ElementAt(i);
                        string2 = fragments.ElementAt(j);
                        mergedString = MergeStrings(string1, string2);
                    }

                    if(mergedString == string.Empty)
                        mergedString = fragments.ElementAt(0) + " " + fragments.ElementAt(1);

                }
            }   
            
            fragments.Remove(string1);
            fragments.Remove(string2);
            fragments.Add(mergedString); 
            
            return fragments;
        }

        private static int CalculateOverlap(String string1, String string2){

            int m = string1.Length;
	        int n = string2.Length;
 
	        int max = 0;
 
	        int[,] dp = new int[m, n];
 
	        for(int i=0; i<m; i++){
		        for(int j=0; j<n; j++){
			        if(string1.ElementAt(i) == string2.ElementAt(j)){
				        if(i==0 || j==0){
					        dp[i, j]=1;
				        }else{
					        dp[i, j] = dp[i-1, j-1]+1;
				        }
 
				        if(max < dp[i, j])
					        max = dp[i, j];
			        }
 
		        }
	        }   
 
	        return max;
            
        }

        private static String MergeStrings(String string1, String string2){

            if(string1.Contains(string2))
                return string1;
            else if(string2.Contains(string1))
                return string2;

            int rightOverlap = 0;
            String stringFromRight = String.Empty;

            // search for best match from right
            for (int i = string2.Length; i > 0; i--) {
                string overlap = string2.Substring(0, i);
                string rest = string2.Substring(i);
                if (string1.EndsWith(overlap, StringComparison.InvariantCulture)) {
                    stringFromRight = string1 + rest;
                    rightOverlap = CalculateOverlap(string1, rest);
                }
            }

            // search for best match from left
            for (int i = 0; i < string2.Length; i++) {
                string overlap = string2.Substring(i);
                string rest = string2.Substring(0, i);
                if (string1.StartsWith(overlap, StringComparison.InvariantCulture) && CalculateOverlap(rest, string1) > rightOverlap) {
                    return rest + string1;
                }
                else if(rightOverlap > CalculateOverlap(rest, string1))
                    return stringFromRight;
            }

            return String.Empty;

        }
    }
}

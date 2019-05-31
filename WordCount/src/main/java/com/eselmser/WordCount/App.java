package com.jselmser.WordCount;
import java.io.*;
import java.util.*;
import java.util.Map.Entry;

public class App 
{
    public static void main( String[] args )
    {
    	//Punctuation
    	List<String> puncList = new ArrayList<String>();
    	puncList.add("!");
    	puncList.add(".");
    	puncList.add("?");
    	puncList.add(",");
    	puncList.add("'");
    	puncList.add("\"");
    	puncList.add(":");
    	puncList.add(";");
    	puncList.add("'");
    	puncList.add("-");
    	puncList.add("[");
    	puncList.add("]");
    	//hashmap
    	Map<String, Integer> wordMap = new HashMap<String, Integer>();
    	
    	//file name
    	String fileName = "src/macbeth.txt";
        //read in file
    	Scanner reader = null;
    	try {
			reader = new Scanner(new File(fileName));
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    	//parse file
    	if(reader != null) {
    		while(reader.hasNext()){
    			String temp = reader.next();
    			for(String s : puncList){ //should go through the entire list of punctuation that is invalid and remove them
    				if(temp.contains(s)){
    					temp = temp.replace(s, "");
    				}
    			}
    			if(temp.length() > 4){ ////elim words with 4 or less letters
    				//check if in dict else add
    				if(wordMap.containsKey(temp)) { ////store remaining in hashmap
    					wordMap.put(temp, (wordMap.get(temp) + 1));
    				} else {
    					wordMap.put(temp, 1);
    				}
    			}
    		}
    	}    	
    	////convert to list, collections.sort
    	Vector<Map.Entry<String,Integer>> sortedWords = new Vector<Entry<String, Integer>>(wordMap.entrySet());
    	Collections.sort(sortedWords, new Comparator<Entry<String, Integer>>(){
    		   public int compare(Entry<String, Integer> a, Entry<String, Integer> b){
    		       return a.getValue().compareTo(b.getValue()); 
    		   }
    		});
    	//test
    	Collections.reverse(sortedWords);
    	System.out.println("The second most common word in Macbeth is " + sortedWords.elementAt(1).getKey() + " and it appears " + sortedWords.elementAt(1).getValue() + " times.");
    	
    }
}

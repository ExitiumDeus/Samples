package com.jselmser.lds;

import java.math.BigDecimal;

import org.apache.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;



public class AppStart {

	public static Logger logger = Logger.getRootLogger();
//
//	@Autowired
//	public static BusinessObject bo;
    public static void main( String[] args )
    {
    	
    	//local run test area without deploying.
    	ApplicationContext appContext = new ClassPathXmlApplicationContext("/WEB-INF/ApplicationContext.xml");
        //create and run the dao?
    	BusinessObject bo = (BusinessObject)appContext.getBean("BusinessObject");
//    	bo.CreateLender("Josh");
//    	bo.CreateLender("Terry");
//    	bo.CreateLender("Melissa");
//    	bo.CreateCommit();
//    	bo.CreateCommit();
    	bo.CreateLoan(bo.FindLenderById(2050),32,"1901","Atl","Ga","30329","Josh",false,1.00,new BigDecimal(1000),false);
    	//System.out.println(bo.FindLenderById(50));
    	System.out.println(bo.toString());
    	
    	
    }

}

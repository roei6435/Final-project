﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness_Club
{
    internal class PersonList
    {

        List<Person> listP;
        public string adminKey = "admins";
        public string userKey = "users";
        public PersonList(List<Person> listP)
        {
            this.listP = listP;
        }

        public Person findPersonById(string userId)
        {
            foreach (Person p in listP)
            {
                if (p.UserId == userId)
                    return p;
            }
            return null;
        }
        public bool checkIfKeyIsProper(string key)
        {
            if(key == adminKey || key== userKey) return true;
            return false;
        }

        //A FUNCTION THAT RECEIVES A LIST OF PERSONS AND SPLITTED INTO TWO LISTS OF ADMINS AND USERS
        public List<Person> getOnlyUsersOrAdminsFromListPerson(string key)
        {
            if (!checkIfKeyIsProper(key)) return null;
            List<Person>newList=new List<Person>();
            if (key == adminKey)
            {
                foreach(Person person in listP)
                {
                    if(person.IsAdmin)
                        newList.Add(person);
                }
                return newList;
            }
            foreach (Person person in listP)
            {
                if (!person.IsAdmin)
                    newList.Add(person);
            }
            return newList;
        }

        //RETURN COUNT ADMINS ADN USERS IN SYSTEM BY KEY
        public int CountAdminsANDUsersInSystem(string key)
        {
            if (!checkIfKeyIsProper(key)) return 0;
            List<Person> newList = getOnlyUsersOrAdminsFromListPerson(key);   
            return newList.Count();
        }

        public int PercentageGenderForUserORAdmins(string key)
        {
            if (!checkIfKeyIsProper(key)) return 0;
            List<Person>newList=getOnlyUsersOrAdminsFromListPerson(key);
            float male = 0;
            foreach (Person person in newList)
            {
                if (person.Gender)
                    male = male + 1.0f;
            }
            return (int)(male / newList.Count() * 100);
        }

        //A FUNCTION THAT RETURNS THE PERCENTAGE OF BLOCKED USERS IN THE SYSTEM.
        public int PercentageIsBlockedForUser()
        {
            float isBlocked = 0;
            foreach (Person person in listP)
            {
                if (!person.IsAdmin && person.IsBlocked)
                    isBlocked = isBlocked + 1.0f;
            }
            return (int)(isBlocked / CountAdminsANDUsersInSystem(userKey) * 100);
        }


        //A FUNCTION THAT RECEIVES A LIST OF EMAILS AND RETURNS IN A STRING WHAT IS THE PREFERRED
        //EMAIL AND IN WHAT PERCENTAGE OF THE TOTAL LIST.
        public string CheckFavoriteMailANDPercentForOneList(List<string> emails)
        {
            List<string> tmp = emails;
            int max = 0; string favEmail = "";
            for (int i = 0; i < tmp.Count - 1; i++)
            {

                int counter = 1;
                for (int j = i + 1; j < tmp.Count; j++)
                {
                    if (tmp[j] == tmp[i])
                    {
                        counter++;
                    }

                }
                if (counter > max)
                {
                    favEmail = tmp[i];
                }

            }
            float percent = emails.Count(s => s == favEmail);  //5
            percent=percent / emails.Count() * 100;       // 5/8*100
            return favEmail + " " + percent.ToString("0");
        }


        //A FUNCTION THAT RECEIVES A LIST OF PERSONALITIES AND BUILDS TWO EMAIL LISTS
        //FOR ADMINISTRATORS AND USERS IN ORDER TO PERFORM ACTIONS.
        public string GetFavoriteEmailAndHowMatchPercent(string key)
        {

            if (key != adminKey && key != userKey)return null;
            List<Person>newList= getOnlyUsersOrAdminsFromListPerson(key);
            List<string> emailsList = new List<string>();
           foreach(Person person in newList)
           {
                string email = person.Email;  //Roei6435@gmail.com
                email = email.Split('@')[1];     //gmail.com
                email = email.Split('.')[0];    //gmail
                emailsList.Add(email);  
           }
           return CheckFavoriteMailANDPercentForOneList(emailsList);
        }

        //A FUNCTION THAT GET A DATE AND RETURNS AN AGE.
        public static float GetAge(string dateOfBirth)
        {
            DateTime dateOfBirth1 = Convert.ToDateTime(dateOfBirth);
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth1.Year * 100 + dateOfBirth1.Month) * 100 + dateOfBirth1.Day;

            return (float)(a - b) / 10000;
        }

        //A FUNCTION THAT RETURNS AVG OF AGES.
        public float getAvgForListOfAdminsOrUsers(string key)
        {
            if (!checkIfKeyIsProper(key)) return 0;
            List<Person> newList = getOnlyUsersOrAdminsFromListPerson(key);
            float sum = 0f;
            foreach(Person person in newList)
            {
                sum += GetAge(person.DateBorn);
            }
            return   sum/ newList.Count();
        }


        //A FUNCTION THAT RETURNS THE MOST EXPERIENCED USER OR ADMINS AND ALSO THE LEAST EXPERIENCED.
        public List<Person> theMostAndLeastExperiencedUserOrAdmins(string key)
        {
            if (!checkIfKeyIsProper(key)) return null;
            List<Person> newList = getOnlyUsersOrAdminsFromListPerson(key);
            Person personMostExprince=newList[0], personLeastExperienced= newList[0];
            float max =GetAge(newList[0].DateRegistion), min = max;
            foreach (Person person in newList)
            {
                if (GetAge(person.DateRegistion) > max)
                {
                    max = GetAge(person.DateRegistion);
                   personMostExprince = person;
                }
                if (GetAge(person.DateRegistion) < min)
                {
                    min = GetAge(person.DateRegistion);
                    personLeastExperienced = person;
                }
            }
            newList.Clear();    
            newList.Add(personMostExprince);
            newList.Add(personLeastExperienced);
            return newList;

        }

        //A FUNCTION THAT RETURN THE MOST OLDER USER OR ADMIN AND ALSO THE MOST YOUNGER.
        public List<Person> theMostOlderAndMostYoungerUserOrAdmin(string key)
        {
            if (!checkIfKeyIsProper(key)) return null;
            List<Person> newList = getOnlyUsersOrAdminsFromListPerson(key);
            Person personMostOlder = newList[0], personMostYounger = newList[0];
            float max = GetAge(newList[0].DateBorn), min = max;
            foreach (Person person in newList)
            {
                if (GetAge(person.DateBorn) > max)
                {
                    max = GetAge(person.DateBorn);
                    personMostOlder = person;
                }
                if (GetAge(person.DateBorn) < min)
                {
                    min = GetAge(person.DateBorn);
                    personMostYounger = person;
                }
            }
            newList.Clear();
            newList.Add(personMostOlder);
            newList.Add(personMostYounger);
            return newList;
        }






    }
}
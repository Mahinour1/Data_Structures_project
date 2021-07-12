#pragma once
#include "string"
#include "queue"
using namespace std;
class Node
{
public:
     string name;
     string value;
     string data;
     //int type;
    //public Node[] children = new Node[10];
     queue<Node*> children;


    Node( string n, string v = "NULL", string d = "NULL")
    {
        name = n;
        value = v;
        data = d;
        
        //type = t;
    }
    Node()
    {
        name = "NULL";
        value = "NULL";
        data = "NULL";
    }
};


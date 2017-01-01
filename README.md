# Turcam
C#, CefSharp based ui, Machine Controller Interface

![alt tag](https://raw.githubusercontent.com/panalgin/turcam/master/Index.jpg)


#This project is currently in a planning phase.
We have two cnc machines which are to be controlled by this interface controller software.
One of the machines is a 2m by 4meters plasma/boring machine that utilizes hypertherm 105 as plasma source and a 9KW 12.000 rpm spindle as milling and boring spindle.
The other machine is a 50cm by 70cm smt pick&place machine which is to be opensourced soon.

#This software is to supersede Mach3 software. 
We want to control our machines with our own controller board (whose software and design schematics will be opensourced too). Our controller board uses chipkit max32 as its main processor. We develop an input/output handling pcb based on that PIC32MX CPU, using arduino alike environment.
First milestone is to use that control board to drive up to 6 axes which are throttled by step motors. We do not plan to utilise encoders or servo motor handling before the 1.0.0 version. Our first milestone also consists of
boring holes from 3mm diameter to 26mm diameter. We will mostly use standart sheet metal materials ranging from 1mm thickness to 40mm thickness. Stainless steel and aluminium will also be supported.

We plan to prepare a release version containing all the stuff that is needed to control any custom cnc machine soon.

Issues, pull-requests and questions are welcomed!

Regards

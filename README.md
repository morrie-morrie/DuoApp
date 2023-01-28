# DuoApp
C# Console app which connects you to the Cisco DUO Accounts API. This is used by MSP's for multi-tenancy access to their customers Duo tenacy.

There is an example.config file. Rename and put it into the DuoApp root directory with the following changes:

ApiHost = your Duo API Host for the Duo Application
ApiKey = The intergration Key for the Duo Application
ApiSecret = The Secret for the Duo Application

You should read the following documentation from Duo in order to configure your application. This should be in your Administration Parent Account. (It'll be the first one you log into.
https://duo.com/docs/accountsapi

This is a work in progress. I've uploaded it at this stage, because I found it difficult to find simple working examples of this running in .Net 7 and Visual Studio 2022.
It's currently very simple.
My next step is to further refactor the code so that I can call other endpoints easily.
First target it to make a list of the individual users for each tenant.

I've got a working system under powershell, which does this, converts to Excel worksheet, including a Pivot table and then can email it out.
I wanted to be able to add this into our Custom database at work and wanted to give a working example to our Devs.

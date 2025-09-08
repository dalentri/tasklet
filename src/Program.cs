using System;
using System.Text.Json;

// Import all other files
// using tasklet.src.Data;
using tasklet.src.Models;

namespace tasklet.Cli
{
    class Program
    {
        static void Main(string[] args)
        {

            // If the user didnt type anything yet
            if (args.Length == 0)
            {
                Console.WriteLine("Welcome to tasklet!");
                Console.WriteLine("Type 'tasklet help' for a list of commands");
                return;
            }

            //TODO: Check for json file, if not present, create a new one

            // handles the first argument
            switch (args[0])
            {
                // Adds a new task to the list
                // Steps:
                // 1. Read the existing json file for tasks (or create new one)
                // 2. Deserialize the list of pre-existing tasks
                // 3. Add the new task to the list
                // 4. Serialize the list back into json
                // 5. Write new list to the json file
                case "add":
                    // Checks to see if tasks.json is in the 
                    if (!File.Exists("tasks.json"))
                    {
                        File.WriteAllText("tasks.json", "[]");
                    }
                    else if (File.Exists("tasks.json"))
                    {
                        string taskList = File.ReadAllText("tasks.json");
                    }

                    // Creates a new task object 
                    tasklet.src.Models.Task newTask = new tasklet.src.Models.Task();

                    // Assign variables to the new string
                    // newTask.Id = 
                    args[1] = newTask.Description;
                    newTask.Status = "not-started";
                    newTask.createdAt = DateTime.Now;
                    newTask.updatedAt = DateTime.Now;



                    File.WriteAllText("tasks.json", JsonSerializer.Serialize(newTask));
                    break;
                case "update":
                    int index = Int32.Parse(args[1]);
                    string newDescription = args[2];
                    break;
                case "delete":
                    break;
                // Marks the task's status
                // Syntax: tasklet mark not-started
                // Possible values: not-started -> in-progress -> done
                case "mark":
                    break;
                case "list":
                    break;
                default:
                    break;
            }
        }
    }
}

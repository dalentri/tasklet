using System;
using System.Text.Json;


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


            // Makes sure data file exists before handling commands
            if (!File.Exists("tasks.json"))
            {
                File.WriteAllText("tasks.json", "[]");

            }

            string taskListJSON = File.ReadAllText("tasks.json");
            List<tasklet.src.Models.Task> taskList = JsonSerializer.Deserialize<List<tasklet.src.Models.Task>>(taskListJSON);

            // handles the first argument
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "help":
                        Console.WriteLine("Commands");
                        Console.WriteLine("--------------------------------\n");
                        Console.WriteLine("help, \tOpens the command menu");
                        Console.WriteLine("add \"Task description\", \tAdds a task to the task list");
                        Console.WriteLine("update [index] \"New task description\", \tUpdates a pre-existing task");
                        Console.WriteLine("list, \tLists all tasks");
                        Console.WriteLine("list [status], \tLists tasks by status");
                        Console.WriteLine("delete [index], \tDeletes a task at the desired index");
                        Console.WriteLine("mark [status], \tChanges the status of a task");
                        break;

                    // Adds a new task to the list
                    // Steps:
                    // 1. Read the existing json file for tasks (or create new one)
                    // 2. Deserialize the list of pre-existing tasks
                    // 3. Add the new task to the list
                    // 4. Serialize the list back into json
                    // 5. Write new list to the json file
                    case "add":

                        if (args.Length != 2)
                        {
                            Console.WriteLine("Incorrect usage of 'add'");
                            Console.WriteLine("Correct usage is 'add \"description\"'");
                            return;
                        }


                        // Creates a new task object 
                        tasklet.src.Models.Task newTask = new tasklet.src.Models.Task();

                        // Assign variables to the new string
                        newTask.Id = taskList.Count + 1;
                        newTask.Description = args[1];
                        newTask.Status = "not-started";
                        newTask.CreatedAt = DateTime.Now;
                        newTask.UpdatedAt = DateTime.Now;

                        taskList.Add(newTask);

                        // Serialize and write the task
                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(taskList));

                        Console.WriteLine(newTask.Description + " added at index " + newTask.Id);
                        break;
                    case "update":
                        int desiredUpdateIndex = Int32.Parse(args[1]);
                        string newDescription = args[2];

                        // Checks to see if tasks.json is in the file system
                        if (!File.Exists("tasks.json"))
                        {
                            Console.WriteLine("Data not found.");
                        }
                        else if (File.Exists("tasks.json"))
                        {

                            // Check if index is even valid
                            if (taskList.Count == 0 || desiredUpdateIndex > taskList.Count)
                            {
                                Console.WriteLine("Index " + desiredUpdateIndex + " was unable to be found.");
                                return;
                            }

                            foreach (var task in taskList)
                            {
                                if (task.Id == desiredUpdateIndex)
                                {
                                    task.Description = newDescription;
                                }
                            }
                        }

                        break;
                    //TODO: Finish delete command
                    // Desired functionality:
                    // delete all
                    // delete [index]
                    case "delete":

                        // Check for file Existence
                        if (!File.Exists("tasks.json"))
                        {
                            Console.WriteLine("Task data not found.");
                            return;
                        }


                        // argument check
                        if (args[1] == "all")
                        {
                            string deleteArg = args[1];
                            File.WriteAllText("tasks.json", []);
                            return;
                        }

                        int deleteIndex = Int32.Parse(args[1]);

                        if (taskList.Count == 0 || deleteIndex > taskList.Count)
                        {
                            Console.WriteLine("Index " + deleteIndex + " was unable to be found.");
                            return;
                        }

                        foreach (var task in taskList)
                        {
                            if (task.Id == deleteIndex)
                            {

                            }
                        }



                        break;

                    case "mark":
                        string[] allowedStatus = { "not-started", "in-progress", "finished" };

                        string newStatus = args[1];
                        int desiredMarkIndex = Int32.Parse(args[2]);

                        if (!allowedStatus.Contains(newStatus))
                        {
                            Console.WriteLine("Invalid status");
                            return;
                        }

                        // Checks it tasks.json exists before preceeding
                        if (!File.Exists("tasks.json"))
                        {
                            Console.WriteLine("Task data not found.");
                            return;
                        }
                        else if (File.Exists("tasks.json"))
                        {

                            // Check if index is even valid
                            if (taskList.Count == 0 || desiredMarkIndex > taskList.Count)
                            {
                                Console.WriteLine("Index was unable to be found.");
                                return;
                            }

                            foreach (var task in taskList)
                            {
                                if (task.Id == desiredMarkIndex)
                                {
                                    task.Status = newStatus;
                                }
                            }
                        }

                        Console.WriteLine("Sucessfully marked task " + desiredMarkIndex + " as " + newStatus);
                        break;
                    case "list":

                        foreach (var task in taskList)
                        {
                            Console.WriteLine(task.Id);
                            if (task.Description == "" || task.Description == null)
                            {
                                Console.WriteLine("[No description provided]");
                            }
                            else
                            {
                                Console.WriteLine(task.Description);
                            }
                            Console.WriteLine(task.Status);
                            Console.WriteLine(task.CreatedAt);
                            Console.WriteLine(task.UpdatedAt);
                            Console.WriteLine("");
                        }
                        break;
                    default:
                        Console.WriteLine("not a valid command.");
                        break;
                }
            }
        }
    }
}

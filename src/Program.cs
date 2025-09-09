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
                        // Makes a new list object to be populated 
                        List<tasklet.src.Models.Task> taskList = new List<tasklet.src.Models.Task>();

                        // Checks to see if tasks.json is in the file system
                        if (!File.Exists("tasks.json"))
                        {
                            File.WriteAllText("tasks.json", "[]");
                        }
                        else if (File.Exists("tasks.json"))
                        {
                            string taskListJSON = File.ReadAllText("tasks.json");
                            taskList = JsonSerializer.Deserialize<List<tasklet.src.Models.Task>>(taskListJSON);
                        }

                        // Creates a new task object 
                        tasklet.src.Models.Task newTask = new tasklet.src.Models.Task();

                        // Assign variables to the new string
                        newTask.Id = taskList.Count + 1;
                        newTask.Description = args[1];
                        newTask.Status = "not-started";
                        newTask.createdAt = DateTime.Now;
                        newTask.updatedAt = DateTime.Now;

                        taskList.Add(newTask);

                        // Serialize and write the task
                        File.WriteAllText("tasks.json", JsonSerializer.Serialize(taskList));

                        Console.WriteLine(newTask.Description + " added at index " + newTask.Id);
                        break;
                    case "update":
                        int index = Int32.Parse(args[1]);
                        string newDescription = args[2];
                        break;
                    case "delete":
                        break;
                    //TODO: Mark command
                    // Marks the task's status
                    // Syntax: tasklet mark not-started
                    // Possible values: not-started -> in-progress -> done
                    case "mark":
                        string newStatus = args[0];
                        int desiredIndex = Int32.Parse(args[1]);

                        // Checks it tasks.json exists before preceeding
                        if (!File.Exists("tasks.json"))
                        {
                            Console.WriteLine("Task data not found.");
                            return;
                        }
                        else if (File.Exists("tasks.json"))
                        {
                            string taskListJSON = File.ReadAllText("tasks.json");
                            taskList = JsonSerializer.Deserialize<List<tasklet.src.Models.Task>>(taskListJSON);

                            // Check if index is even valid
                            if (taskList.Count == 0 || desiredIndex > taskList.Count)
                            {
                                Console.WriteLine("Index was unable to be found.");
                                return;
                            }


                        }
                        break;
                    case "list":
                        if (!File.Exists("tasks.json"))
                        {
                            Console.WriteLine("Tasks list is empty");
                        }
                        else if (File.Exists("tasks.json"))
                        {
                            string taskListJSON = File.ReadAllText("tasks.json");
                            taskList = JsonSerializer.Deserialize<List<tasklet.src.Models.Task>>(taskListJSON);

                            foreach (var item in taskList)
                            {
                                Console.WriteLine(item.Id + item.Description + item.Status + item.createdAt + item.updatedAt);
                            }
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

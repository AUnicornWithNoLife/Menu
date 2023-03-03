namespace ConsoleMenu
{
    public class Menu
    {
        private static ConsoleKey[] inputKeys =
        {
                ConsoleKey.D1,
                ConsoleKey.D2,
                ConsoleKey.D3,
                ConsoleKey.D4,
                ConsoleKey.D5,
                ConsoleKey.D6,
                ConsoleKey.D7,
                ConsoleKey.D8,
                ConsoleKey.D9,
        };

        private static ConsoleKey pageUp = ConsoleKey.UpArrow;
        private static ConsoleKey pageDown = ConsoleKey.DownArrow;

        public static int NumberMenu(string[] menuItems, string inputPrompt = "", ConsoleKey? menuReturn = null)
        {
            int width = 0, page = 0, maxPage = (int)((((float)menuItems.Length) / 9f) + .9f);  // MAX PAYNE - absolute horrible

            List<List<string>> pageItems = new List<List<string>>();

            for (int x = 0; x < maxPage; x++)
            {
                List<string> pi = new List<string>();

                for (int y = 0; y < 9; y++)
                {
                    int xy = (x * 9) + y;

                    if (menuItems.Length <= xy)
                        break;

                    pi.Add(menuItems[xy]);
                }

                pageItems.Add(pi);
            }

            foreach (List<string> pageItem in pageItems)
                foreach (string menuItem in pageItem)
                    if (menuItem.Length > width)
                        width = menuItem.Length;

            for (int x = 0; x < pageItems.Count; x++)
                for (int y = 0; y < pageItems[x].Count; y++)
                    pageItems[x][y] =
                        "[ " + (y + 1).ToString() + ":  " + pageItems[x][y] +
                        String.Concat(Enumerable.Repeat(" ", (width - pageItems[x][y].Length) + 1)) + "]"; // please just trust me that this 'works'

            while (true)
            {
                Console.Clear();

                for (int i = 0; i < pageItems[page].Count; i++)
                    Console.WriteLine(pageItems[page][i]);

                Console.WriteLine();

                if (maxPage > 1)
                    Console.WriteLine("[PAGE : " + (page + 1).ToString() + " / " + (maxPage).ToString() + "]" + Environment.NewLine);

                if (menuReturn != null)
                    Console.WriteLine("Press [" + menuReturn.ToString() + "] to go back" + Environment.NewLine);

                Console.Write((inputPrompt == "") ? "Option: " : inputPrompt);

                while (true)
                {
                    ConsoleKey input = Console.ReadKey().Key;

                    if (input == menuReturn)
                        return -1;

                    if (input == pageUp)
                    {
                        if (page-- <= 0) page = 0;
                        else break;
                    }

                    if (input == pageDown)
                    {
                        if (page++ >= (maxPage - 1)) page = maxPage - 1;
                        else break;
                    }

                    if (input == ConsoleKey.Enter)
                        break;

                    for (int i = 1; i <= pageItems[page].Count; i++) // always do this last as it is least efficient
                        if (input == inputKeys[i - 1])
                            return (page * 9) + i;
                }
            }
        }
    }
}
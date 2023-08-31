using Labb1.Models;
using Labb1.Models.DTOs;

namespace Labb1.Services
{
    public class BookComparer
    {
        public static bool Compare(Book book, BookCreateDTO bookCreateDTO)
        {
            if (book.Title == bookCreateDTO.Title)
            {
                if (book.Author == bookCreateDTO.Author)
                {
                    if (book.Description == bookCreateDTO.Description)
                    {
                        if (book.Year == bookCreateDTO.Year)
                        {
                            if (book.Genre == bookCreateDTO.Genre)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}

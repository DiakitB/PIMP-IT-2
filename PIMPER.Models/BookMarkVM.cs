using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMPER.Models
{
	public class BookMarkVM
	{
		public IEnumerable<UserBookMark> favoriteList { get; set; }
	}
}

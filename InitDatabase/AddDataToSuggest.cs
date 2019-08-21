using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace InitDatabase
{
    [Migration(20190820200001)]
    public class AddDataToSuggest : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Insert.IntoTable("suggest").Row(new { text = "Привет" });
            Insert.IntoTable("suggest").Row(new { text = "Приток" });
            Insert.IntoTable("suggest").Row(new { text = "Простота" });
            Insert.IntoTable("suggest").Row(new { text = "Приказ" });
            Insert.IntoTable("suggest").Row(new { text = "Прут" });
            Insert.IntoTable("suggest").Row(new { text = "Пруд" });
            Insert.IntoTable("suggest").Row(new { text = "Проволока" });
            Insert.IntoTable("suggest").Row(new { text = "Пристав" });
            Insert.IntoTable("suggest").Row(new { text = "Предок" });
            Insert.IntoTable("suggest").Row(new { text = "Приз" });

            Insert.IntoTable("suggest").Row(new { text = "Промзона" });
            Insert.IntoTable("suggest").Row(new { text = "Просо" });
            Insert.IntoTable("suggest").Row(new { text = "Признание" });

            Insert.IntoTable("suggest").Row(new { text = "Апрель" });
            Insert.IntoTable("suggest").Row(new { text = "Озон" });
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Net;

namespace Saree.User
{
    public partial class Invoice : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userid"] != null)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        rOrderItem.DataSource = GetOrderDetails();
                        rOrderItem.DataBind();

                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        DataTable GetOrderDetails()
        {
            double GrandTotal = 0;
            con = new SqlConnection(Connection.GetConnection());
            cmd = new SqlCommand("Invoice", con);
            cmd.Parameters.AddWithValue("@Action", "GETINVOICEBYID");
            cmd.Parameters.AddWithValue("@PaymentID", Convert.ToInt32(Request.QueryString["id"]));
            cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow drow in dt.Rows)
                {
                    GrandTotal += Convert.ToDouble(drow["TotalPrice"]);
                }
            }
            DataRow dr = dt.NewRow();
            dr["TotalPrice"] = GrandTotal;
            return dt;

        }

        protected void lbDownloadInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                string DownloadPath = Environment.GetFolderPath (Environment.SpecialFolder.UserProfile) + @"\Downloads\Order_Invoice.pdf";
                //string DownloadPath = @"D:\Order_Invoice.pdf";
                DataTable dtbl = GetOrderDetails();
                ExportToPdf2(dtbl, DownloadPath, "Order Invoice");

                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(DownloadPath);

                if (buffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                }

            }
            catch (Exception ex)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Error : " + ex.Message.ToString();
            }
        }

        void ExportToPdf(DataTable dtblTable, string strPdfPath, string strHeader)
        {
            // Using statement to ensure FileStream is properly disposed
            using (FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                // Create the document and writer outside the using statement
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                try
                {
                    document.Open();

                    // Report Header
                    BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    Font fntHead = new Font(bfntHead, 16, 0, Color.GRAY);
                    Paragraph prgHeading = new Paragraph
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
                    document.Add(prgHeading);

                    // Author
                    Paragraph prgAuthor = new Paragraph
                    {
                        Alignment = Element.ALIGN_RIGHT
                    };
                    BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    Font fntAuthor = new Font(btnAuthor, 8, 0, Color.GRAY);
                    prgAuthor.Add(new Chunk("Order From: Momai Silk", fntAuthor));
                    prgAuthor.Add(new Chunk("\nOrder Date: " + dtblTable.Rows[0]["OrderDate"].ToString(), fntAuthor));
                    document.Add(prgAuthor);

                    // Line separator
                    Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, Color.BLACK, Element.ALIGN_CENTER, -1)));
                    document.Add(p);

                    // Add line break
                    document.Add(new Chunk("\n", fntHead));

                    // Write the table
                    PdfPTable table = new PdfPTable(dtblTable.Columns.Count - 2);

                    // Table header
                    BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    Font fntColumnHeader = new Font(btnColumnHeader, 9, 0, Color.WHITE);
                    for (int i = 0; i < dtblTable.Columns.Count - 2; i++)
                    {
                        PdfPCell cell = new PdfPCell
                        {
                            BackgroundColor = Color.GRAY
                        };
                        cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                        table.AddCell(cell);
                    }

                    // Table data
                    Font fntColumnData = new Font(btnColumnHeader, 8, 0, Color.BLACK);
                    for (int i = 0; i < dtblTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtblTable.Columns.Count - 2; j++)
                        {
                            PdfPCell cell = new PdfPCell();
                            cell.AddElement(new Chunk(dtblTable.Rows[i][j].ToString(), fntColumnData));
                            table.AddCell(cell);
                        }
                    }

                    document.Add(table);
                }
                finally
                {
                    if (document.IsOpen())
                    {
                        document.Close();
                    }

                    writer.Close();
                }
            }
        }
        void ExportToPdf2(DataTable dtblTable, string strPdfPath, string strHeader)
        {
            using (FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document(PageSize.A4, 36, 36, 54, 54);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);


                try
                {
                    document.Open();

                    PdfPTable headerTable = new PdfPTable(1);
                    headerTable.WidthPercentage = 100;

                    PdfPCell headerCell = new PdfPCell(new Phrase("|| Shree Ganeshay Namah: ||\n\nMOMAI SILK\n\n7070 TO 7074, 7TH FLOOR AVADH TEXTILE MARKET, NEAR SAHARA DARWAJA SURAT\n\nPhone: 9737541361, 9016441361  Email: momaisilk7073@gmail.com  GSTIN: 24ABSFM9485A1ZV\n\n", FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Color.BLACK)))
                    {
                        Border = Rectangle.BOX,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    headerTable.AddCell(headerCell);

                    document.Add(headerTable);

                    PdfPTable buyerTable = new PdfPTable(1);
                    buyerTable.WidthPercentage = 100;

                    PdfPCell buyerCell = new PdfPCell(new Phrase("M/s " + dtblTable.Rows[0]["CustomerName"].ToString() + "\n\nGST No: None", FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.NORMAL, Color.BLACK)))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        PaddingTop = 20
                    };
                    buyerTable.AddCell(buyerCell);

                    document.Add(buyerTable);

                    PdfPTable boxTable = new PdfPTable(1);
                    boxTable.WidthPercentage = 20;
                    

                    PdfPCell boxCell = new PdfPCell();
                    boxCell.Border = Rectangle.NO_BORDER;
                    boxCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    boxCell.PaddingTop = 70;


                    
                    boxCell.AddElement(new Phrase("Challan: 0" + dtblTable.Rows[0]["ChallanCode"].ToString(), FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Color.BLACK)));
                    boxCell.AddElement(new Phrase("Date: " + dtblTable.Rows[0]["OrderDate"].ToString(), FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, Font.NORMAL, Color.BLACK)));

                    boxTable.AddCell(boxCell);

                    boxTable.TotalWidth = 150; 
                    boxTable.WriteSelectedRows(0, -1, document.Right - boxTable.TotalWidth, document.Top - 100, writer.DirectContent);

                    Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 0.0F, Color.BLACK, Element.ALIGN_CENTER, -1)));
                    document.Add(p);

                    document.Add(new Chunk("\n\n", FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.NORMAL, Color.BLACK)));

                    PdfPTable table = new PdfPTable(6 + 1); 
                    table.WidthPercentage = 100;
                    BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    Font fntColumnHeader = new Font(btnColumnHeader, 9, 0, Color.WHITE);
                    for (int i = 0; i < 6; i++)
                    {
                        PdfPCell cell = new PdfPCell
                        {
                            BackgroundColor = Color.GRAY,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                        table.AddCell(cell);
                    }

                    PdfPCell totalHeaderCell = new PdfPCell(new Phrase("TOTAL", fntColumnHeader))
                    {
                        BackgroundColor = Color.GRAY,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    };
                    table.AddCell(totalHeaderCell);

                    Font fntColumnData = new Font(btnColumnHeader, 8, 0, Color.BLACK);
                    decimal grandTotal = 0;
                    for (int i = 0; i < dtblTable.Rows.Count; i++)
                    {
                        decimal rowTotal = 0;
                        for (int j = 0; j < 6; j++)
                        {
                            PdfPCell cell = new PdfPCell
                            {
                                HorizontalAlignment = Element.ALIGN_CENTER
                            };
                            cell.AddElement(new Chunk(dtblTable.Rows[i][j].ToString(), fntColumnData));
                            table.AddCell(cell);

                            if (j == 6 - 1) 
                            {
                                if (decimal.TryParse(dtblTable.Rows[i][j].ToString(), out decimal amount))
                                {
                                    rowTotal = amount;
                                }
                                else
                                {
                                    rowTotal = 0;
                                }
                            }
                        }

                        PdfPCell rowTotalCell = new PdfPCell(new Phrase(rowTotal.ToString("F2"), fntColumnData))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        table.AddCell(rowTotalCell);

                        grandTotal += rowTotal;
                    }

                    document.Add(table);

                    PdfPTable totalTable = new PdfPTable(1);
                    totalTable.WidthPercentage = 100;

                    PdfPCell grandTotalCell = new PdfPCell(new Phrase("Grand Total: " + grandTotal.ToString("F2"), FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.BOLD, Color.BLACK)))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };
                    totalTable.AddCell(grandTotalCell);

                    document.Add(totalTable);

                    PdfPTable signatureTable = new PdfPTable(2);
                    signatureTable.WidthPercentage = 100;
                    signatureTable.SetWidths(new float[] { 1, 1 });

                    PdfPCell receiverSignCell = new PdfPCell(new Phrase("Receiver's Sign", FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.NORMAL, Color.BLACK)))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        PaddingTop = 50
                    };
                    signatureTable.AddCell(receiverSignCell);

                    PdfPCell authorisedSignCell = new PdfPCell(new Phrase("For, MOMAI SILK\n\n\n\n\nAuthorised Signatory", FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.NORMAL, Color.BLACK)))
                    {
                        Border = Rectangle.NO_BORDER,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    };
                    signatureTable.AddCell(authorisedSignCell);

                    document.Add(signatureTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    if (document.IsOpen())
                    {
                        document.Close();
                    }
                    writer.Close();
                }
            }
        }
    }
    public class CustomPageEventHelper : PdfPageEventHelper
    {
        private string _header;
        private string _footer;

        public CustomPageEventHelper(string header, string footer)
        {
            _header = header;
            _footer = footer;
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {

            PdfPTable footerTable = new PdfPTable(1);
            footerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            PdfPCell footerCell = new PdfPCell(new Phrase(_footer, FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, Font.NORMAL, Color.GRAY)))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT
            };
            footerTable.AddCell(footerCell);
            footerTable.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin - 20, writer.DirectContent);
        }
    }
}

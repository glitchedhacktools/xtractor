using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xtractor
{
    public partial class Form1 : Form
    {
        byte[] ROM;
        string path;
        string fileNameROM;
        bool isFC = false;
        bool isColor = false;
        bool isSong1 = false;
        bool isSong2 = false;
        int songNum = 0;
        bool isFD = false;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void xtractnSave(object sender, EventArgs e)
        {
            string textString = "";
            string result = "";
            int i, z = 0;
            int offset = (int)numericUpDown1.Value;
            int max = (int)numericUpDown2.Value;
            bool hasText = false;
            progressBar1.Maximum = (int)numericUpDown2.Value;
            progressBar1.Minimum = (int)numericUpDown1.Value;
            progressBar1.Value = progressBar1.Minimum;
            label2.Visible = true;
            currentOffset.Visible = true;
            button1.Enabled = false;
            button2.Enabled = false;
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            onlyTextCheck.Enabled = false;
            result += "OFFSET;CHANGED (Y/N);REPOINTED;STRING" + Environment.NewLine;
            Application.DoEvents();
            for (i = offset; i<max; i++)
            {
                if (ROM[i] == 0xFF)
                {
                    if (onlyTextCheck.Checked && textString == "")
                    {
                        offset = i + 1;
                        textString = "";
                    }
                    else
                    {
                        result += "0x" + offset.ToString("X") + ";Y;;" + textString + Environment.NewLine;
                        offset = i + 1;
                        textString = "";
                    }
                    hasText = false;
                    progressBar1.Value = offset;
                    currentOffset.Text = offset.ToString("X");
                    Application.DoEvents();
                }
                else
                {
                    if (onlyTextCheck.Checked && hasText == false)
                    {
                        hasText = checkWord(i);
                        if (hasText == false)
                        {
                            do
                            {
                                i++;
                            } while (ROM[i + 1] != 0xFF);
                        }
                        else
                        {
                            textString += HexToString(ROM[i]);
                        }
                    }
                    else
                    {
                        textString += HexToString(ROM[i]);
                    }
                }
            }
            label2.Visible = false;
            currentOffset.Visible = false;
            button1.Enabled = true;
            button2.Enabled = true;
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            onlyTextCheck.Enabled = true;
            Application.DoEvents();
            saveFileDialog1.FileName = fileNameROM.Remove(fileNameROM.Length-4) + "_" + ((int)numericUpDown1.Value).ToString("X") + "-" + ((int)numericUpDown2.Value).ToString("X") + ".csv";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, result, Encoding.GetEncoding(1252));
            }

        }

        private bool checkWord(int i)
        {
            int j, z = 0;
            int len = 16, minVow = 1, minSpace = 0, maxSpace = 4;
            int vowVar = 0, vow = 0, count = 0, sym = 0, spa = 0;
            byte[] array = ROM.Skip(i).Take(len).ToArray();
            bool result = true;
            for (j = 0; j < len; j++)
            {
                switch (array[j])
                {
                    case 0x00: spa++; break;
                    case 0x01: vowVar++; /*À*/  break;
                    case 0x02: vowVar++; /*Á*/  break;
                    case 0x03: vowVar++; /*Â*/  break;
                    case 0x04: vowVar++; /*Ç*/  break;
                    case 0x05: vowVar++; /*È*/  break;
                    case 0x06: vowVar++; /*É*/  break;
                    case 0x07: vowVar++; /*Ê*/  break;
                    case 0x08: vowVar++; /*Ë*/  break;
                    case 0x09: vowVar++; /*Ì*/  break;
                    case 0x0B: vowVar++; /*Î*/  break;
                    case 0x0C: vowVar++; /*Ï*/  break;
                    case 0x0D: vowVar++; /*Ò*/  break;
                    case 0x0E: vowVar++; /*Ó*/  break;
                    case 0x0F: vowVar++; /*Ô*/  break;
                    case 0x11: vowVar++; /*Ù*/  break;
                    case 0x12: vowVar++; /*Ú*/  break;
                    case 0x13: vowVar++; /*Û*/  break;
                    case 0x14: count++; /*Ñ*/  break;
                    case 0x16: vowVar++; /*à*/  break;
                    case 0x17: vowVar++; /*á*/  break;
                    case 0x19: count++; /*ç*/  break;
                    case 0x1A: vowVar++; /*è*/  break;
                    case 0x1B: vowVar++; /*é*/  break;
                    case 0x1C: vowVar++; /*ê*/  break;
                    case 0x1D: vowVar++; /*ë*/  break;
                    case 0x1E: vowVar++; /*ì*/  break;
                    case 0x20: vowVar++; /*î*/  break;
                    case 0x21: vowVar++; /*ï*/  break;
                    case 0x22: vowVar++; /*ò*/  break;
                    case 0x23: vowVar++; /*ó*/  break;
                    case 0x24: vowVar++; /*ô*/  break;
                    case 0x25: vowVar++; /*æ*/  break;
                    case 0x26: vowVar++; /*ù*/  break;
                    case 0x27: vowVar++; /*ú*/  break;
                    case 0x28: vowVar++; /*û*/  break;
                    case 0x29: count++; /*ñ*/  break;
                    case 0x5A: vowVar++; /*Í*/  break;
                    case 0x68: vowVar++; /*â*/  break;
                    case 0x6F: vowVar++; /*í*/  break;
                    case 0xA1: count++; /*0*/  break;
                    case 0xA2: count++; /*1*/  break;
                    case 0xA3: count++; /*2*/  break;
                    case 0xA4: count++; /*3*/  break;
                    case 0xA5: count++; /*4*/  break;
                    case 0xA6: count++; /*5*/  break;
                    case 0xA7: count++; /*6*/  break;
                    case 0xA8: count++; /*7*/  break;
                    case 0xA9: count++; /*8*/  break;
                    case 0xAA: count++; /*9*/  break;
                    case 0xBB: vow++;                   /*A*/  break;
                    case 0xBC: count++; /*B*/  break;
                    case 0xBD: count++; /*C*/  break;
                    case 0xBE: count++; /*D*/  break;
                    case 0xBF: vow++;                   /*E*/  break;
                    case 0xC0: count++; /*F*/  break;
                    case 0xC1: count++; /*G*/  break;
                    case 0xC2: count++; /*H*/  break;
                    case 0xC3: vow++;                   /*I*/  break;
                    case 0xC4: count++; /*J*/  break;
                    case 0xC5: count++; /*K*/  break;
                    case 0xC6: count++; /*L*/  break;
                    case 0xC7: count++; /*M*/  break;
                    case 0xC8: count++; /*N*/  break;
                    case 0xC9: vow++;                   /*O*/  break;
                    case 0xCA: count++; /*P*/  break;
                    case 0xCB: count++; /*Q*/  break;
                    case 0xCC: count++; /*R*/  break;
                    case 0xCD: count++; /*S*/  break;
                    case 0xCE: count++; /*T*/  break;
                    case 0xCF: vow++;                   /*U*/  break;
                    case 0xD0: count++; /*V*/  break;
                    case 0xD1: count++; /*W*/  break;
                    case 0xD2: count++; /*X*/  break;
                    case 0xD3: count++; /*Y*/  break;
                    case 0xD4: count++; /*Z*/  break;
                    case 0xD5: vow++;                   /*a*/  break;
                    case 0xD6: count++; /*b*/  break;
                    case 0xD7: count++; /*c*/  break;
                    case 0xD8: count++; /*d*/  break;
                    case 0xD9: vow++;                   /*e*/  break;
                    case 0xDA: count++; /*f*/  break;
                    case 0xDB: count++; /*g*/  break;
                    case 0xDC: count++; /*h*/  break;
                    case 0xDD: vow++;                   /*i*/  break;
                    case 0xDE: count++; /*j*/  break;
                    case 0xDF: count++; /*k*/  break;
                    case 0xE0: count++; /*l*/  break;
                    case 0xE1: count++; /*m*/  break;
                    case 0xE2: count++; /*n*/  break;
                    case 0xE3: vow++;                   /*o*/  break;
                    case 0xE4: count++; /*p*/  break;
                    case 0xE5: count++; /*q*/  break;
                    case 0xE6: count++; /*r*/  break;
                    case 0xE7: count++; /*s*/  break;
                    case 0xE8: count++; /*t*/  break;
                    case 0xE9: count++;                 /*u*/  break;
                    case 0xEA: count++; /*v*/  break;
                    case 0xEB: count++; /*w*/  break;
                    case 0xEC: count++; /*x*/  break;
                    case 0xED: count++; /*y*/  break;
                    case 0xEE: count++; /*z*/  break;
                    case 0xF1: vowVar++; /*Ä*/  break;
                    case 0xF2: vowVar++; /*Ö*/  break;
                    case 0xF3: vowVar++; /*Ü*/  break;
                    case 0xF4: vowVar++; /*ä*/  break;
                    case 0xF5: vowVar++; /*ö*/  break;
                    case 0xF6: vowVar++; /*ü*/  break;
                    case 0xFF: len = j; break;
                    default: sym++; result = false; break;
                }
            }
            if (vow < minVow) result = false;
            if (spa < minSpace && spa > maxSpace) result = false;
            if (vowVar >= (vow + count)) result = false;
            if (vowVar >= vow) result = false;
            if (sym >= (vow + count)) result = false;
            if (vow > count) result = false;
            return result;
        }

        private void openRom(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                fileNameROM = openFileDialog1.SafeFileName;
                Text = "Glitched Xtractor (" + fileNameROM + ")";
                textBox1.Text = path;
                ROM = File.ReadAllBytes(path);
                button2.Enabled = true;
                numericUpDown1.Enabled = true;
                numericUpDown1.Maximum = ROM.Length-1;
                numericUpDown2.Enabled = true;
                numericUpDown2.Maximum = ROM.Length-1;
                numericUpDown2.Value = ROM.Length-1;
                onlyTextCheck.Enabled = true;
            }
        }
        public string HexToString(int b)
        {
            string result = "";
            string type = "NONE";
            if (isFC == false && isFD == false && isColor == false && isSong1 == false && isSong2 == false)
            {
                if (b >= 0xBB && b <= 0xD4) type = "MAYUS";
                if (b >= 0xD5 && b <= 0xEE) type = "MINUS";
                if (b >= 0xA1 && b <= 0xAA) type = "NUM";
                switch (b)
                {
                    case 0x00: return " ";
                    case 0x01: return "À";
                    case 0x02: return "Á";
                    case 0x03: return "Â";
                    case 0x04: return "Ç";
                    case 0x05: return "È";
                    case 0x06: return "É";
                    case 0x07: return "Ê";
                    case 0x08: return "Ë";
                    case 0x09: return "Ì";
                    case 0x0B: return "Î";
                    case 0x0C: return "Ï";
                    case 0x0D: return "Ò";
                    case 0x0E: return "Ó";
                    case 0x0F: return "Ô";
                    case 0x10: return "Œ";
                    case 0x11: return "Ù";
                    case 0x12: return "Ú";
                    case 0x13: return "Û";
                    case 0x14: return "Ñ";
                    case 0x15: return "ß";
                    case 0x16: return "à";
                    case 0x17: return "á";
                    case 0x19: return "ç";
                    case 0x1A: return "è";
                    case 0x1B: return "é";
                    case 0x1C: return "ê";
                    case 0x1D: return "ë";
                    case 0x1E: return "ì";
                    case 0x20: return "î";
                    case 0x21: return "ï";
                    case 0x22: return "ò";
                    case 0x23: return "ó";
                    case 0x24: return "ô";
                    case 0x25: return "œ";
                    case 0x26: return "ù";
                    case 0x27: return "ú";
                    case 0x28: return "û";
                    case 0x29: return "ñ";
                    case 0x2A: return "º";
                    case 0x2B: return "ª";
                    case 0x2C: return "ᵉʳ";
                    case 0x2D: return "&";
                    case 0x2E: return "+";
                    case 0x34: return "[LV]";
                    case 0x35: return "=";
                    case 0x36: return ";";
                    case 0x50: return "▯";
                    case 0x51: return "¿";
                    case 0x52: return "¡";
                    case 0x53: return "[PK]";
                    case 0x54: return "[MN]";
                    case 0x55: return "[PO]";
                    case 0x56: return "[KE]";
                    case 0x57: return "[BL]";
                    case 0x58: return "[OC]";
                    case 0x59: return "[K]";
                    case 0x5A: return "Í";
                    case 0x5B: return "%";
                    case 0x5C: return "(";
                    case 0x5D: return ")";
                    case 0x68: return "â";
                    case 0x6F: return "í";
                    case 0x79: return "[U]";
                    case 0x7A: return "[D]";
                    case 0x7B: return "[L]";
                    case 0x7C: return "[R]";
                    case 0x84: return "ᵉ";
                    case 0x85: return "<";
                    case 0x86: return ">";
                    case 0xA0: return "ʳᵉ";
                    case 0xAB: return "!";
                    case 0xAC: return "?";
                    case 0xAD: return ".";
                    case 0xAE: return "-";
                    case 0xAF: return "·";
                    case 0xB0: return "…";
                    case 0xB1: return "“";
                    case 0xB2: return "”";
                    case 0xB3: return "‘";
                    case 0xB4: return "’";
                    case 0xB5: return "♂";
                    case 0xB6: return "♀";
                    case 0xB7: return "₽";
                    case 0xB8: return ",";
                    case 0xB9: return "×";
                    case 0xBA: return "/";
                    case 0xEF: return "▶";
                    case 0xF0: return ":";
                    case 0xF1: return "Ä";
                    case 0xF2: return "Ö";
                    case 0xF3: return "Ü";
                    case 0xF4: return "ä";
                    case 0xF5: return "ö";
                    case 0xF6: return "ü";
                    case 0xFA: return "\\l";
                    case 0xFB: return "\\p";
                    case 0xFC: isFC = true; return "";
                    case 0xFD: isFD = true; return "";
                    case 0xFE: return "\\n";
                    default:
                        switch (type)
                        {
                            case "MAYUS": result += (char)(b - 0x7A); return result;
                            case "MINUS": result += (char)(b - 0x74); return result;
                            case "NUM": result += (char)(b - 0x71); return result;
                            default: result += "[h" + b.ToString("X2") + "]"; return result;
                        }
                }
            }
            if (isColor)
            {
                isColor = false;
                return b.ToString("X2") + "]";
            }

            if (isSong2)
            {
                int songResult;
                isSong2 = false;
                songNum += b * 0x100;
                songResult = songNum;
                songNum = 0;
                return songResult.ToString();
            }

            if (isSong1)
            {
                isSong1 = false;
                isSong2 = true;
                songNum += b;
                return songNum.ToString();
            }

            if (isFC)
            {
                isFC = false;
                switch (b)
                {
                    case 0x01: isColor = true; return "[color";
                    case 0x02: isColor = true; return "[highlight";
                    case 0x03: isColor = true; return "[shadowcolor";
                    case 0x08: return "[pause";
                    case 0x09: return "[pausebutton]";
                    case 0x10: isSong1 = true; return "[playsong";
                    case 0x17: return "[pausesong]";
                    case 0x18: return "[resumesong]";
                    default: return "[hFC]";
                }
            }

            if (isFD)
            {
                isFD = false;
                switch (b)
                {
                    case 0x1: return "[player]";
                    case 0x6: return "[rival]";
                    default: return "[v" + b + "]";
                }
            }
            return "";
        }

    }
}

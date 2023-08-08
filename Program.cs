using System;
using System.Collections.Generic;

class Program
{
    static List<Barang> inventaris = new List<Barang>();
    static List<Transaksi> riwayatTransaksi = new List<Transaksi>();

    static void Main()
    {
        bool isRunning = true;

        Console.WriteLine("Selamat datang di Aplikasi Kasir!");
        Console.WriteLine("================================");

        // Menambahkan contoh barang ke inventaris
        inventaris.Add(new Barang("001", "Pensil", 5000, 50));
        inventaris.Add(new Barang("002", "Buku Tulis", 10000, 30));
        inventaris.Add(new Barang("003", "Penghapus", 3000, 20));

        while (isRunning)
        {
            TampilkanMenu();

            Console.Write("Pilih opsi: ");
            string opsi = Console.ReadLine() ?? "";

            switch (opsi)
            {
                case "1":
                    TambahBarang();
                    break;
                case "2":
                    BeliBarang();
                    break;
                case "3":
                    LihatStokBarang();
                    break;
                case "4":
                    LihatRiwayatTransaksi();
                    break;
                case "5":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Opsi tidak valid.");
                    break;
            }

            Console.WriteLine();
        }

        Console.WriteLine("Terima kasih telah menggunakan Aplikasi Kasir!");
    }

    static void TampilkanMenu()
    {
        Console.WriteLine("Pilih opsi:");
        Console.WriteLine("1. Tambah Barang ke Inventaris");
        Console.WriteLine("2. Beli Barang");
        Console.WriteLine("3. Lihat Stok Barang");
        Console.WriteLine("4. Lihat Riwayat Transaksi");
        Console.WriteLine("5. Keluar");
    }

    static void TambahBarang()
    {
        Console.Write("Masukkan kode barang: ");
        string kode = Console.ReadLine() ?? "";

        Console.Write("Masukkan nama barang: ");
        string nama = Console.ReadLine() ?? "";

        Console.Write("Masukkan harga barang: ");
        int harga = int.Parse(Console.ReadLine() ?? "");

        Console.Write("Masukkan stok barang: ");
        int stok = int.Parse(Console.ReadLine() ?? "");

        inventaris.Add(new Barang(kode, nama, harga, stok));
        Console.WriteLine("Barang berhasil ditambahkan ke inventaris.");
    }

    static void BeliBarang()
    {
        Console.Write("Masukkan kode barang yang ingin dibeli: ");
        string kode = Console.ReadLine() ?? "";

        Barang barang = inventaris.FirstOrDefault(b => b.Kode == kode) ?? new Barang("", "", 0, 0);

        if (barang == null)
        {
            Console.WriteLine("Barang tidak ditemukan.");
            return;
        }

        Console.Write("Masukkan jumlah barang yang ingin dibeli: ");
        int jumlahBeli = int.Parse(Console.ReadLine() ?? "");

        if (jumlahBeli > barang.Stok)
        {
            Console.WriteLine("Stok barang tidak mencukupi.");
            return;
        }

        int totalHarga = barang.Harga * jumlahBeli;

        barang.Stok -= jumlahBeli;

        riwayatTransaksi.Add(new Transaksi(barang, jumlahBeli, totalHarga));

        Console.WriteLine(
            $"Barang {barang.Nama} sebanyak {jumlahBeli} buah berhasil dibeli. Total harga: {totalHarga}"
        );
    }

    static void LihatStokBarang()
    {
        Console.WriteLine("Stok Barang:");
        Console.WriteLine("============");

        foreach (Barang barang in inventaris)
        {
            Console.WriteLine($"{barang.Nama} (Kode: {barang.Kode}) - Stok: {barang.Stok}");
        }
    }

    static void LihatRiwayatTransaksi()
    {
        Console.WriteLine("Riwayat Transaksi:");
        Console.WriteLine("==================");

        foreach (Transaksi transaksi in riwayatTransaksi)
        {
            Console.WriteLine(
                $"{transaksi.Barang.Nama} (Kode: {transaksi.Barang.Kode}) - Jumlah: {transaksi.JumlahBeli} - Total Harga: {transaksi.TotalHarga}"
            );
        }
    }
}

class Barang
{
    public string Kode { get; }
    public string Nama { get; }
    public int Harga { get; }
    public int Stok { get; set; }

    public Barang(string kode, string nama, int harga, int stok)
    {
        Kode = kode;
        Nama = nama;
        Harga = harga;
        Stok = stok;
    }
}

class Transaksi
{
    public Barang Barang { get; }
    public int JumlahBeli { get; }
    public int TotalHarga { get; }

    public Transaksi(Barang barang, int jumlahBeli, int totalHarga)
    {
        Barang = barang;
        JumlahBeli = jumlahBeli;
        TotalHarga = totalHarga;
    }
}
